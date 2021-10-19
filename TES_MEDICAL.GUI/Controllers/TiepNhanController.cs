using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Controllers
{
    public class TiepNhanController : Controller
    {
        private readonly ITiepNhan _service;
        private readonly IChuyenKhoa _chuyenkhoaRep;
       
        public TiepNhanController(
            ITiepNhan service,
            IChuyenKhoa chuyenKhoaRep
            
            
            )
        {
            _service = service;
            _chuyenkhoaRep = chuyenKhoaRep;
            
        }
        public IActionResult Index()
        {
            return View("ThemPhieuKham");
        }
        
        public async Task<IActionResult> ThemPhieuKham(string MaPhieu)

        {
            ViewBag.ListCK = new SelectList(await _chuyenkhoaRep.GetAll(), "MaCK", "TenCK");
            var model = await _service.GetPhieuDatLichById(MaPhieu);
            if(!string.IsNullOrWhiteSpace(MaPhieu))

            ViewBag.BenhNhan = new BenhNhan { HoTen = model.TenBN, NgaySinh = model.NgaySinh, SDT = model.SDT, Email = model.Email };
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
           
            return View();
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

        public async Task<IActionResult> Edit(string id)
        {
            var result = await _service.GetPhieuDatLichById(id);
            if (result == null)
            {
                return NotFound(); ;
            }
            else
            {

                return PartialView("_PartialViewEditLich", result);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PhieuDatLich model)
        {

            if (await _service.Edit(model) != null)
                return Json(new { status = 1, title = "", text = "Cập nhật thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Cập nhật không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());


        }

        [HttpGet]
        public async Task<IActionResult> ReloadPage()
        {
            var listDatLich = await _service.GetAllPhieuDatLich();
            return Json(listDatLich, new JsonSerializerSettings());
        }

    }
}
