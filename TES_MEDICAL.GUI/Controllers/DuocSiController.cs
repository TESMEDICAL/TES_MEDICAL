using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Interfaces;

namespace TES_MEDICAL.GUI.Controllers
{
    public class DuocSiController : Controller
    {
        private readonly IDuocSi _service;
        public DuocSiController(IDuocSi service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ReloadPage(byte TrangThai)
        {
            var listToaThuocCTT = await _service.GetAllToaThuocCTT(TrangThai);
            return Json(listToaThuocCTT);
            //return Ok(listToaThuocCTT);
        }

        public async Task<IActionResult> ChiTietToaThuoc(Guid MaPhieu)
        {
            var toaThuoc = await _service.GetToaThuocByMaPhieu(MaPhieu);
            ViewBag.CTToaThuoc = await _service.GetChiTiet(MaPhieu);

            return PartialView("_ChiTietToaThuoc", toaThuoc);
        }

        public async Task<IActionResult> ChangeSoUuTien(Guid maPK)
        {
            if (await _service.ChangeSoUuTien(maPK) != null)
                return Json(new { status = 1, title = "", text = "Thay đổi thành công."}, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Thay đổi thất bại."}, new Newtonsoft.Json.JsonSerializerSettings());
        }

        [HttpPost]
        public async Task<IActionResult> ThanhToan(Guid maPK)
        {
            var result = await _service.ThanhToanThuoc(maPK);
            if (result != null)
            {
                return Json(new { status = 1, title = "", text = "Thanh toán thành công.", redirectUrL = Url.Action("ToaThuocDangPhat", "DuocSi"), obj = "" }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { status = -2, title = "", text = "Thanh toán không thành công", obj = "" }, new JsonSerializerSettings());
            }
        }


        public IActionResult Index()
        {
            return View("ToaThuoc");
        }

        public IActionResult ToaThuoc()
        {
            return View();
        }

        

        public IActionResult ToaThuocDangPhat()
        {
            return View();
        }

        public IActionResult ChiTietDangPhat()
        {
            return PartialView("_ChiTietDangPhat");
        }

        public IActionResult LichSuThuoc()
        {
            return View();
        }
        public IActionResult ChiTietLichSu()
        {
            return PartialView("_ChiTietLichSu");
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
