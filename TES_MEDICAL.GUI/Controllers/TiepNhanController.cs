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
            return View();
        }

        public IActionResult ThemPhieuKham()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ThemDichVu()
        {
            return PartialView("_AddDichVu");
        }
    }
}
