using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Controllers
{
    public class QuanLyNguoiDungController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ThemNguoiDung()
        {
            return PartialView("_ThemNguoiDung");
        }

        public IActionResult EditNguoiDung()
        {
            return PartialView("_EditNguoiDung");
        }

        public IActionResult ChiTietNguoiDung()
        {
            return PartialView("_ChiTietNguoiDung");
        }
    }
}
