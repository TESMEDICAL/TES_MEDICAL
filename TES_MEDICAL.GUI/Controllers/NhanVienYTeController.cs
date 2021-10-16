using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Controllers
{
    public class NhanVienYTeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ThemNvYTe()
        {
            return PartialView("_AddNhanVienYTe");
        }

        public IActionResult EditNhanVienYTe()
        {
            return PartialView("_EditNhanVienYTe");
        }

        public IActionResult DetailNhanVienYTe()
        {
            return PartialView("_DetailNhanVienYTe");
        }
    }
}
