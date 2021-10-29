﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Controllers
{
    public class DuocSiController : Controller
    {
        private readonly IDuocSi _service;
        private readonly IThuoc _thuocService;


        public DuocSiController(IDuocSi service, IThuoc thuocService)
        {
            _service = service;
            _thuocService = thuocService;
        }

        [HttpGet]
        public async Task<IActionResult> ReloadPage(byte TrangThai)
        {
            var listToaThuocCTT = await _service.GetAllToaThuocCTT(TrangThai);
            return Json(listToaThuocCTT);
            //return Ok(listToaThuocCTT);
        }
        

        public async Task<IActionResult> ChangeSoUuTien(Guid maPK)
        {
            if (await _service.ChangeSoUuTien(maPK) != null)
                return Json(new { status = 1, title = "", text = "Thay đổi thành công."}, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Thay đổi thất bại."}, new Newtonsoft.Json.JsonSerializerSettings());
        }

        //Hàm thanh toán thuốc dùng trong Model ToaThuoc
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


        //Hàm xác nhận thuốc dùng trong Model ToaThuocDangPhat
        [HttpPost]
        public async Task<IActionResult> XacNhanThuocDangCho(Guid maPK)
        {
            var result = await _service.XacNhanThuocDangCho(maPK);
            if (result != null)
            {
                return Json(new { status = 1, title = "", text = "Xác nhận thành công", redirectUrL = Url.Action("LichSuThuoc", "DuocSi"), obj = "" }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { status = -2, title = "", text = "Xác nhận không thành công", obj = "" }, new JsonSerializerSettings());
            }
        }


        public IActionResult Index()
        {
            return View("ToaThuoc");
        }



        // ToaThuoc/Index
        [Authorize]
        public IActionResult ToaThuoc()
        {
            return View();
        }

        public async Task<IActionResult> ChiTietToaThuoc(Guid MaPhieu)
        {
            var toaThuoc = await _service.GetToaThuocByMaPhieu(MaPhieu);
            ViewBag.CTToaThuoc = await _service.GetChiTiet(MaPhieu);

            return PartialView("_ChiTietToaThuoc", toaThuoc);
        }


        // Toathuoc/ToaThuocDangPhat
        public IActionResult ToaThuocDangPhat()
        {
            return View();
        }

        public async Task<IActionResult> ChiTietDangPhat(Guid MaPhieu)
        {
            var toaThuoc = await _service.GetToaThuocByMaPhieu(MaPhieu);
            ViewBag.CTToaThuocDangPhat = await _service.GetChiTiet(MaPhieu);
            return PartialView("_ChiTietDangPhat", toaThuoc);
        }



        public IActionResult LichSuThuoc()
        {
            return View();
        }
        public async Task<IActionResult> ChiTietLichSu(Guid MaPhieu)
        {
            var toaThuoc = await _service.GetToaThuocByMaPhieu(MaPhieu);
            ViewBag.CTLichSuThuoc = await _service.GetChiTiet(MaPhieu);
            return PartialView("_ChiTietLichSu", toaThuoc);
        }

        public async Task<IActionResult> DanhSachThuoc(ThuocSearchModel model)
        {
            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = await _thuocService.SearchByCondition(model);

            ViewBag.Names = listPaged;
            ViewBag.Data = model;
            return View(new ThuocSearchModel());
        }


        [HttpGet]
        public async Task<IActionResult> PageList(ThuocSearchModel model)
        {

            var listmodel = await _thuocService.SearchByCondition(model);
            if (listmodel.Count() > 0)
            {

                if (!model.Page.HasValue) model.Page = 1;




                ViewBag.Names = listmodel;
                ViewBag.Data = model;

                return PartialView("_NameListThuoc", listmodel);
                //return View("DanhSachThuoc", listmodel);
            }
            else
            {

                return Json(new { status = -2, title = "", text = "Không tìm thấy", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }


        }



        public async Task<IActionResult> ChiTietThuoc(Guid id)
        {
            if (await _thuocService.Get(id) == null)
            {
                return NotFound(); ;
            }
            else
            {


                return PartialView("_ChiTietThuoc", await _thuocService.Get(id));
            }
        }

    }
}
