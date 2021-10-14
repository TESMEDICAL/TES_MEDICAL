﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Controllers
{
    public class DuocSiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ToaThuoc()
        {
            return View();
        }

        public IActionResult ChiTietToaThuoc()
        {
            return PartialView("_ChiTietToaThuoc");
        }

        public IActionResult ToaThuocDangPhat()
        {
            return View();
        }

        public IActionResult ChiTietDangPhat()
        {
            return PartialView("_ChiTietDangPhat");
        }
    }
}
