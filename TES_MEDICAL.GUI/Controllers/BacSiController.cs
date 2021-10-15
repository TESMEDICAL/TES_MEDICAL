using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Controllers
{
    public class BacSiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PhieuKham()
        {
            return View();
        }

        public IActionResult KhamBenh()
        {
            return View();
        }

        public IActionResult LichSuKham()
        {
            return PartialView("_LichSuKham");
        }

        public IActionResult DanhSachThuoc()
        {
            return View();
        }

        public IActionResult ChiTietThuoc()
        {
            return PartialView("_ChiTietThuoc");
        }
    }
}
