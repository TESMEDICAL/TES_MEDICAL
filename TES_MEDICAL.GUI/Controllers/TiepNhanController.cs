using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Interfaces;

namespace TES_MEDICAL.GUI.Controllers
{
    public class TiepNhanController : Controller
    {
        private readonly ITiepNhan _service;
        public TiepNhanController(ITiepNhan service)
        {
            _service = service;
        }
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

        public async Task<IActionResult> QuanLyDatLich()
        {
            var listDatLich = await _service.GetAllPhieuDatLich();
            return View(listDatLich);
        }

        public IActionResult CapNhatDichVu()
        {
            ViewBag.Current = "capnhatdichvu";
            return View();
        }

        public async Task<IActionResult> ChiTietDatLich(string id)
        {
            var chiTietDatLich = await _service.GetPhieuDatLichById(id);
            return PartialView("_ChiTietDatLich", chiTietDatLich);
        }


    }
}
