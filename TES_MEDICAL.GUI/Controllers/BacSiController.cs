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
    public class BacSiController : Controller
    {
        private readonly IKhamBenh _khambenhRep;
        private readonly IThuoc _thuocRep;
        public BacSiController (IKhamBenh khambenhRep,IThuoc thuocRep)
        {
            _khambenhRep = khambenhRep;
            _thuocRep = thuocRep;
        }
       

        
        [HttpGet]
        public async Task<IActionResult> ReloadPage(string MaBS)
        {
            var listPK = await _khambenhRep.GetList(MaBS);
            return Json(listPK, new JsonSerializerSettings());
        }

        [Route("/bacsi")]
        [Route("/bacsi/Phieukham")]
        public async Task<IActionResult>PhieuKham()
        {
            return View();
        }

        public async Task<IActionResult> KhamBenh(string MaPK)
        {
            var item = await _khambenhRep.GetPK(Guid.Parse(MaPK));
            item.NgayTaiKham = item.NgayKham.AddDays(7);
            ViewBag.LichSuKham = item.MaBNNavigation.PhieuKham.Where(x => x.MaPK.ToString() != MaPK).ToList()??new List<PhieuKham>();
            ViewBag.PhieuKham = JsonConvert.SerializeObject(item, Formatting.Indented,
new JsonSerializerSettings
{
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
});
            return View(item);
            
        }

        public async Task<IActionResult> GetListThuoc()
        {

            return PartialView("_partialToaThuoc", await _khambenhRep.GetAllThuoc());
        
        }
        public async Task<IActionResult> GetToaThuoc(string MaPK)
        {
            var item = await _khambenhRep.GetPK(Guid.Parse(MaPK));
            return PartialView("_XacNhanKetQua", item);
        }
        [HttpPost]
        public async Task<IActionResult> ThemToa(PhieuKham model)
        {
            foreach(var item in model.ToaThuoc.ChiTietToaThuoc)
            {
                item.GhiChu = $"Ngày uống {item.LanTrongNgay} lần, mỗi lần {item.VienMoiLan},uống {(item.TruocKhian ? "trước khi ăn":"sau khi ăn")},Uống {(item.Sang ? "Sáng," : "")}{(item.Trua ? ", trưa" : "")}{(item.Chieu ? ", chieu" : "")}";
            }    
            var result = await _khambenhRep.AddToaThuoc(model,false);

            if (result != null)
            {
                //var stt = new STTViewModel { STT = result.MaPKNavigation.STTPhieuKham.STT, HoTen = result.MaPKNavigation.MaBNNavigation.HoTen, UuTien = result.MaPKNavigation.STTPhieuKham.MaUuTien, MaPK = result.MaPK };
                //await _hubContext.Clients.All.SendAsync("SentDocTor", model.MaBS, stt);


                return Json(new { status = 1, title = "", text = "Gửi thành công.", redirectUrL = Url.Action("PhieuKham", "BacSi"), obj = "" }, new JsonSerializerSettings());
            }

            else
                return Json(new { status = -2, title = "", text = "Gửi không thành công", obj = "" }, new JsonSerializerSettings());
        }

      
        [HttpPost]
        public async Task<IActionResult> XacNhanKetQua(PhieuKham model)
        {
              foreach(var item in model.ToaThuoc.ChiTietToaThuoc)
            {
                item.MaThuocNavigation = new Thuoc();
                item.MaThuocNavigation = (await _thuocRep.Get(item.MaThuoc));
            }    
            return PartialView("_XacNhanKetQua",model);
        }

        public IActionResult DanhSachThuoc()
        {
            return View();
        }

        public IActionResult ChiTietThuoc()
        {
            return PartialView("_ChiTietThuoc");
        }

        public IActionResult ThemThuoc()
        {
            return PartialView("_ThemThuoc");
        }
    }
}
