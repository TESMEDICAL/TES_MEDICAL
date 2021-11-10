using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
                return RedirectToAction("Index", "TinTuc");
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
                if(nguoidung.TrangThai == true)
                {
                    if (nguoidung != null)
                    {
                        HttpContext.Session.SetString(SessionKey.Nguoidung.UserName, nguoidung.Email);
                        HttpContext.Session.SetString(SessionKey.Nguoidung.FullName, nguoidung.HoTen);
                        HttpContext.Session.SetString(SessionKey.Nguoidung.ChucVu, nguoidung.ChucVu.ToString());
                        HttpContext.Session.SetString(SessionKey.Nguoidung.NguoidungContext,
                            JsonConvert.SerializeObject(nguoidung));

                        return RedirectToAction("Index", "TinTuc");
                    }
                }
                else
                {
                    return RedirectToAction("NoneUser", "Admin");

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

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
