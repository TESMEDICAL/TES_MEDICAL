using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.SearchModel;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Controllers
{
    [Authorize(Roles = "bacsi")]
    public class BacSiController : Controller
    {
        private readonly IKhamBenh _khambenhRep;
        private readonly IThuoc _thuocRep;
        private readonly UserManager<NhanVienYte> _userManager;
        public BacSiController (IKhamBenh khambenhRep,IThuoc thuocRep, UserManager<NhanVienYte> userManager)
        {
            _khambenhRep = khambenhRep;
            _thuocRep = thuocRep;
            _userManager = userManager;
        }
       

        
        [HttpGet]
        public async Task<IActionResult> ReloadPage()
        {
            var MaBS = (await _userManager.GetUserAsync(User)).Id;
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

        public async Task<JsonResult> GetJsonPK(string MaPK)
        {
            var item = await _khambenhRep.GetPK(Guid.Parse(MaPK));

            return Json(JsonConvert.SerializeObject(item, Formatting.Indented,
new JsonSerializerSettings
{
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
}));

          
        }
        [HttpPost]
        public async Task<IActionResult> ThemToa(PhieuKham model)
        {
            foreach(var item in model.ToaThuoc.ChiTietToaThuoc)
            {
                item.GhiChu = $"Ngày uống {item.LanTrongNgay} lần, mỗi lần {item.VienMoiLan},uống {(item.TruocKhian ? "trước khi ăn":"sau khi ăn")},Uống {(item.Sang ? "Sáng" : "")}{(item.Trua ? ", trưa" : "")}{(item.Chieu ? ", chieu" : "")}.";
            }    
            var result = await _khambenhRep.AddToaThuoc(model);

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

        [HttpPost]
        public async Task<IActionResult> ReLoadThuoc(PhieuKham model)
        {

            var listhuocExist = model.ToaThuoc.ChiTietToaThuoc;
            var listNew = await _khambenhRep.GetAllThuoc();
            var listThuoc = listNew.Where(x => !listhuocExist.Any(y => y.MaThuoc == x.MaThuoc));
            ViewBag.Thuoc = listThuoc;
            return PartialView("_partialToaThuocOld", model.ToaThuoc);
        }

        [HttpGet]
        public async Task<IActionResult> LichSuKham(PhieuKhamSearchModel model)
        {
            model.MaBS = (await _userManager.GetUserAsync(User)).Id;
            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = await _khambenhRep.SearchByCondition(model);

            ViewBag.Names = listPaged;
            ViewBag.Data = model;
            return View();
        }

        public async Task<IActionResult> PagePhieuKham(PhieuKhamSearchModel model)
        {
            model.MaBS = (await _userManager.GetUserAsync(User)).Id;
            var listmodel = await _khambenhRep.SearchByCondition(model);
           
                if (!model.Page.HasValue) model.Page = 1;




                ViewBag.Names = listmodel;
                ViewBag.Data = model;

                return PartialView("_LichSuKham", listmodel);
                //return View("DanhSachThuoc", listmodel);
          
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
