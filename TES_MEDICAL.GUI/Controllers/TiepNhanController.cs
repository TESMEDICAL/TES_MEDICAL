
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
using System.IO;
using SelectPdf;

namespace TES_MEDICAL.GUI.Controllers
{
    public class TiepNhanController : Controller
    {
        private readonly ITiepNhan _service;
        private readonly IChuyenKhoa _chuyenkhoaRep;
        private readonly IDichVu _dichvuRep;
        private readonly INhanVienYte _nhanvienyteRep;
       
        public TiepNhanController(
            ITiepNhan service,
            IChuyenKhoa chuyenKhoaRep,
            IDichVu dichvuRep,
            INhanVienYte nhanVienYteRep
            
            
            )
        {
            _service = service;
            _chuyenkhoaRep = chuyenKhoaRep;
            _dichvuRep = dichvuRep;
            _nhanvienyteRep = nhanVienYteRep;
            
        }
        [Route("/TiepNhan")]
        [Route("/TiepNhan/ThemPhieuKham")]
        
        public async Task<IActionResult> ThemPhieuKham(string MaPhieu)

        {
            ViewBag.ListCK = new SelectList(await _chuyenkhoaRep.GetAll(), "MaCK", "TenCK");
            var model = await _service.GetPhieuDatLichById(MaPhieu);
            if(!string.IsNullOrWhiteSpace(MaPhieu))

            ViewBag.BenhNhan = new BenhNhan { HoTen = model.TenBN, NgaySinh = model.NgaySinh, SDT = model.SDT, Email = model.Email };
            return View();
        }


        public async Task<JsonResult> DocTor_Bind(Guid MaCK)
        {
            var list = await _nhanvienyteRep.GetAllBS(MaCK);
            List<SelectListItem> ListBS = new List<SelectListItem>();
            foreach (var item in list)
            {
                ListBS.Add(new SelectListItem { Text = item.HoTen, Value = item.MaNV.ToString() });
            }
            return Json(ListBS, new JsonSerializerSettings());
        }

        [HttpGet]
        public async Task<IActionResult> GetListDV(Guid MaPhieu)
        {
           
            return PartialView("_AddDichVu",await _dichvuRep.GetDichVu(MaPhieu));
        }

        [HttpPost]
        public async Task<IActionResult> XacNhanDichVu([FromForm] PhieuKhamViewModel model)
        {
            ViewBag.BacSi = await _nhanvienyteRep.Get(model.MaBS);
            var result = new PhieuKhamViewModel { MaBS = model.MaBS, HoTen = model.HoTen, SDT = model.SDT, GioiTinh = model.GioiTinh, NgaySinh = model.NgaySinh, TrieuChung = model.TrieuChung, DiaChi = model.DiaChi };
            result.dichVus = new List<DichVu>();

            foreach(var item in model.dichVus)
            {
                result.dichVus.Add(await _dichvuRep.Get(item.MaDV));
            }
            
            return PartialView("_XacNhanDichVu",result);
        }


        [HttpPost]
        public async Task<IActionResult> FinalCheckOut(PhieuKhamViewModel model)
        {
            var listDichVu = "";
            foreach (var item in model.dichVus)
            {
                var Dv = await _dichvuRep.Get(item.MaDV);
                listDichVu += $"<tr><td class='col-6'><strong>{Dv.TenDV}</strong></td><td class='col-6 text-end'><strong>{Dv.DonGia}</strong></td></tr>";
            }

            HoaDon hoaDon = new HoaDon();
            hoaDon.MaHoaDon = Guid.NewGuid();
            hoaDon.MaNV = Guid.Parse("C29652E8-F9DB-4BF8-B2FD-FF8518E611F3");

            var root = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot");
            using (var reader = new System.IO.StreamReader(root + @"/Invoce.html"))
            {
                string readFile = reader.ReadToEnd();
                string html = string.Empty;
                html = readFile;
                html = html.Replace("{MaHoaDon}", hoaDon.MaHoaDon.ToString());
                html = html.Replace("{MaNhanVien}", hoaDon.MaNV.ToString());
                html = html.Replace("{NgayKham}", DateTime.Now.ToString());
                html = html.Replace("{HoTen}", model.HoTen);
                html = html.Replace("{NgaySinh}", model.NgaySinh.ToString());
                html = html.Replace("{SDT}", model.SDT);
                html = html.Replace("{DiaChi}", model.DiaChi);
                html = html.Replace("{listDichVu}", listDichVu);

                HtmlToPdf ohtmlToPdf = new HtmlToPdf();
                PdfDocument opdfDocument = ohtmlToPdf.ConvertHtmlString(html);
                byte[] pdf = opdfDocument.Save();
                opdfDocument.Close();

                var filePdf = File(
                    pdf,
                    "application/pdf",
                    "StudentList.pdf"
                    );

                string filePath = "";
                filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\HoaDon", "StudentList.pdf");
                System.IO.File.WriteAllBytes(filePath, pdf);           
                

                //return File(
                //    pdf,
                //    "application/pdf",
                //    "StudentList.pdf"
                //    );
            }
            return Json(new { status = 1, title = "", text = "Thêm thành công.", obj = "" }, new JsonSerializerSettings());

            //    if (await _service.CreatePK(model) != null)
            //{
            //    return Json(new { status = 1, title = "", text = "Thêm thành công.", redirectUrL = Url.Action("ThemPhieuKham", "TiepNhan"), obj = "" }, new JsonSerializerSettings());
            //}
            //else
            //    return Json(new { status = -2, title = "", text = "Thêm không thành công", obj = "" }, new JsonSerializerSettings());




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
