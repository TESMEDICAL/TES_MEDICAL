using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Controllers
{
    public class QuanLyTrangChuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ThemBaiViet()
        {
            return View();
        }

        public IActionResult ChinhSuaBaiViet()
        {
            return View();
        }

        public IActionResult XemBaiViet()
        {
            return PartialView("_PreviewBaiViet");
        }
    }
}
