using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ADMIN.Shared.Helper;
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

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return PartialView("_ChangePasswordUser");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _nguoidungSvc.Get(Guid.Parse(HttpContext.Session.GetString(SessionKey.Nguoidung.MaNguoiDung)));
                if (user.MatKhau == MaHoaHelper.Mahoa(model.CurrentPassword))
                {
                    user.MatKhau = MaHoaHelper.Mahoa(model.NewPassword);
                }
                else
                {
                    ModelState.AddModelError("CurrentPassword", "Mật khẩu cũ không đúng.");
                    return PartialView("_ChangePasswordUser",model);
                }
               
                var result = await _nguoidungSvc.Edit(user);
                if (result.errorCode == 0)
                {
                    
                    HttpContext.Session.Clear();
                    return Json(new { status = 1, title = "", text = "Cập nhật thành công.", redirectUrL = Url.Action("Index", "TinTuc"), obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());

                }
                else
                {
                    return Json(new { status = -2, title = "", text = "Cập nhật không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                }
            }
            return PartialView("_ChangePasswordUser",model);
        }



        [HttpGet]
        public async Task<IActionResult> ChangeInfo()
        {
            string EmailNguoiDung = HttpContext.Session.GetString(SessionKey.Nguoidung.UserName);
            string HoTenNguoiDung = HttpContext.Session.GetString(SessionKey.Nguoidung.FullName);
            string hinhAnhNguoiDung = HttpContext.Session.GetString(SessionKey.Nguoidung.HinhAnh);
            string sdtNguoiDung = HttpContext.Session.GetString(SessionKey.Nguoidung.SDT);
            var model = new UpdateUser { Email = EmailNguoiDung, HoTen = HoTenNguoiDung, HinhAnh = hinhAnhNguoiDung, SDT = sdtNguoiDung };
            return PartialView("_Edit_User", model);
        }


        [HttpPost]
        public async Task<ActionResult> ChangeInfo(UpdateUser model, [FromForm] IFormFile file) 
        {
            if (ModelState.IsValid)
            {
                var user = await _nguoidungSvc.Get(Guid.Parse(HttpContext.Session.GetString(SessionKey.Nguoidung.MaNguoiDung)));
                string filePath = "";
                if (file != null)
                {

                    var fileName = Path.GetFileName(DateTime.Now.ToString("ddMMyyyyss") + file.FileName);
                    user.HinhAnh = fileName;
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                }

                              
                user.HoTen = model.HoTen;
                user.SDT = model.SDT;

                var result = await _nguoidungSvc.Edit(user);
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
                    HttpContext.Session.Clear();
                    return Json(new { status = 1, title = "", text = "Cập nhật thành công.", redirectUrL = Url.Action("Index", "TinTuc"), obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                    
                }
                else
                {
                    return Json(new { status = -2, title = "", text = "Cập nhật không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                }
            }
            return PartialView("_Edit_User", model);
        }
    }
}
