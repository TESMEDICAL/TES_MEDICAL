using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Controllers
{
    public class TiepNhanController : Controller
    {
        public IActionResult Index()
        {
            return View("ThemPhieuKham");
        }

        public IActionResult ThemPhieuKham()
        {
            ViewBag.Current = "themphieukham";
            return View();
        }

        [HttpGet]
        public IActionResult ThemDichVu()
        {
            return PartialView("_AddDichVu");
        }

        [HttpGet]
        public IActionResult XacNhanDichVu()
        {
            return PartialView("_XacNhanDichVu");
        }

        public IActionResult QuanLyDatLich()
        {
            ViewBag.Current = "quanlydatlich";
            return View();
        }

        public IActionResult CapNhatDichVu()
        {
            ViewBag.Current = "capnhatdichvu";
            return View();
        }
    }
}
