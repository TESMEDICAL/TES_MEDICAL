
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TES_MEDICAL.GUI.Models.ViewModel;

using TES_MEDICAL.GUI.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using TES_MEDICAL.ENTITIES.Models.ViewModel;
using TES_MEDICAL.SHARE.Models.ViewModel;
using System.IO;
using SelectPdf;
using System.Threading;

namespace TES_MEDICAL.GUI.Controllers
{
    public class TiepNhanController : Controller
    {
        private readonly ITiepNhan _service;
        private readonly IChuyenKhoa _chuyenkhoaRep;
        private readonly IDichVu _dichvuRep;
        private readonly INhanVienYte _nhanvienyteRep;
        private readonly IHubContext<RealtimeHub> _hubContext;
      
        public TiepNhanController(
            ITiepNhan service,
            IChuyenKhoa chuyenKhoaRep,
            IDichVu dichvuRep,
            INhanVienYte nhanVienYteRep,
            IHubContext<RealtimeHub> hubContext
           


            )
        {
            _service = service;
            _chuyenkhoaRep = chuyenKhoaRep;
            _dichvuRep = dichvuRep;
            _nhanvienyteRep = nhanVienYteRep;
            _hubContext = hubContext;
           
            
        }
       
     
        
        public async Task<IActionResult> ThemPhieuKham(string MaPhieu)

        {
            ViewBag.ListCK = new SelectList(await _chuyenkhoaRep.GetAll(), "MaCK", "TenCK");

            ViewBag.ListDV = await _dichvuRep.GetDichVu(Guid.Empty);

            var model = await _service.GetPhieuDatLichById(MaPhieu);
           
            if (!string.IsNullOrWhiteSpace(MaPhieu))
            {
                var phieuKham = new PhieuKhamViewModel { HoTen = model.TenBN, SDT = model.SDT, Email = model.Email, NgaySinh = model.NgaySinh };
                return View(phieuKham);
            }
            return View(new PhieuKhamViewModel());
             
             
        }


        public async Task<JsonResult> DocTor_Bind(Guid MaCK)
        {
            var list = await _nhanvienyteRep.GetAllBS(MaCK);
            List<SelectListItem> ListBS = new List<SelectListItem>();
            foreach (var item in list)
            {
                ListBS.Add(new SelectListItem { Text = item.HoTen, Value = item.Id.ToString() });
            }
            return Json(ListBS, new JsonSerializerSettings());
        }

        [HttpGet]
        public async Task<IActionResult> GetListDV(Guid MaPhieu)
        {
           
            return PartialView("_AddDichVu",await _dichvuRep.GetDichVu(MaPhieu));
        }

        [HttpPost]
        public async Task<IActionResult> XacNhanDichVu(PhieuKhamViewModel model)
        {
                ViewBag.BacSi = await _nhanvienyteRep.Get(model.MaBS.ToString());
                var result = new PhieuKhamViewModel { MaBS = model.MaBS, HoTen = model.HoTen, SDT = model.SDT, GioiTinh = model.GioiTinh, NgaySinh = model.NgaySinh, TrieuChung = model.TrieuChung, DiaChi = model.DiaChi };
                result.dichVus = new List<DichVu>();

                foreach (var item in model.dichVus)
                {
                    result.dichVus.Add(await _dichvuRep.Get(item.MaDV));
                }
                return PartialView("_XacNhanDichVu", result);

        }


        [HttpPost]
        public async Task<IActionResult> FinalCheckOut(PhieuKhamViewModel model)
        {

            var result = await _service.CreatePK(model);

                    if (result != null)
                    {
                var stt = new STTViewModel { STT = result.MaPKNavigation.STTPhieuKham.STT, HoTen = result.MaPKNavigation.MaBNNavigation.HoTen, UuTien = result.MaPKNavigation.STTPhieuKham.MaUuTien, MaPK = result.MaPK };
                await _hubContext.Clients.All.SendAsync("SentDocTor",model.MaBS,stt );
               

                return Json(new { status = 1, title = "", text = "Thêm thành công.", redirectUrL = Url.Action("ThemPhieuKham", "TiepNhan"), obj = "" }, new JsonSerializerSettings());
                    }

                    else
                        return Json(new { status = -2, title = "", text = "Thêm không thành công", obj = "" }, new JsonSerializerSettings());
                


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
        public IActionResult ThemDichVuMoi()
        {
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
