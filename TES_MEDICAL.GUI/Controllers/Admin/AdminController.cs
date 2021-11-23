using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.ViewModel;
using TES_MEDICAL.GUI.Constant;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Controllers.Admin
{
    
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private INguoiDung _nguoidungSvc;

        public AdminController (IWebHostEnvironment webHostEnvironment, INguoiDung nguoidungSvc)
        {
            _webHostEnvironment = webHostEnvironment;
            _nguoidungSvc = nguoidungSvc;
        }

        public IActionResult Login(string returnUrl)
        {
            string userName = HttpContext.Session.GetString(SessionKey.Nguoidung.UserName);
            if (userName != null && userName != "")
            {
                return RedirectToAction("Index", "Report");
            }

            #region Hiển thị Login
            AdminLoginViewModel login = new AdminLoginViewModel();
            login.ReturnUrl = returnUrl;
            return View(login);
            #endregion
        }

        public IActionResult NoneUser()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AdminLoginViewModel viewLogin)
        {
            if (ModelState.IsValid)
            {
                NguoiDung nguoidung = _nguoidungSvc.Login(viewLogin);
                if (nguoidung != null)
                {
                    if (nguoidung.TrangThai == true)
                    {
                        if (nguoidung != null)
                        {
                            HttpContext.Session.SetString(SessionKey.Nguoidung.MaNguoiDung, nguoidung.MaNguoiDung.ToString());
                            HttpContext.Session.SetString(SessionKey.Nguoidung.UserName, nguoidung.Email);
                            HttpContext.Session.SetString(SessionKey.Nguoidung.FullName, nguoidung.HoTen);
                            HttpContext.Session.SetString(SessionKey.Nguoidung.ChucVu, nguoidung.ChucVu.ToString());
                            HttpContext.Session.SetString(SessionKey.Nguoidung.SDT, nguoidung.SDT);
                            HttpContext.Session.SetString(SessionKey.Nguoidung.HinhAnh, nguoidung.HinhAnh);
                            HttpContext.Session.SetString(SessionKey.Nguoidung.NguoidungContext,
                                JsonConvert.SerializeObject(nguoidung));

                            return RedirectToAction("Index", "Report");
                        }
                    }
                    else
                    {
                        return RedirectToAction("NoneUser", "Admin");

                    }
                }
                else
                {
                    ViewBag.Error = "Đăng nhập thất bại.";
                }
               
                
            }
            return View(viewLogin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Admin");
        }


        [HttpPost]
        public async Task<ActionResult> Edit(NguoiDung model, [FromForm] IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string filePath = "";
                if (file != null)
                {

                    var fileName = Path.GetFileName(DateTime.Now.ToString("ddMMyyyyss") + file.FileName);
                    model.HinhAnh = fileName;
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                }

                var result = await _nguoidungSvc.Edit(model);
                if (result.errorCode == -1)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại");
                    return PartialView("_partialedit", model);
                }

                if (result.errorCode == 0)
                {
                    if (file != null)
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                    }
                    return Json(new { status = 1, title = "", text = "Cập nhật thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                }
                else
                {
                    return Json(new { status = -2, title = "", text = "Cập nhật không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                }
            }
            return PartialView("_partialedit", model);
        }
    }
}
