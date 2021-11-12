using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.SearchModel;
using TES_MEDICAL.GUI.Infrastructure;
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
        private readonly IHubContext<SignalServer> _hubContext;
        private readonly ITienIch _tienichRep;

        public BacSiController(
            IKhamBenh khambenhRep,
            IThuoc thuocRep,
            UserManager<NhanVienYte> userManager,
            IHubContext<SignalServer> hubContext,
            ITienIch tienichRep
            )
            
        {
            _khambenhRep = khambenhRep;
            _thuocRep = thuocRep;
            _userManager = userManager;
            _hubContext = hubContext;
            _tienichRep = tienichRep;
            
        }
       

        
        [HttpGet]
        public async Task<IActionResult> ReloadPageSTT(PhieuKhamSearchModel model)
        {
            
            model.MaBS = (await _userManager.GetUserAsync(User)).Id;
            model.TrangThai = 0;
            var listmodel = await _khambenhRep.SearchByCondition(model);

            if (!model.Page.HasValue) model.Page = 1;



            ViewBag.Page = model.Page;
            ViewBag.Names = listmodel;
            ViewBag.Data = model;

          
          
            return PartialView("_listSTTPhieuKham", listmodel);
        }

        [Route("/bacsi")]
        [Route("/bacsi/Phieukham")]
        public async Task<IActionResult>PhieuKham(PhieuKhamSearchModel model)
        {
            model.MaBS = (await _userManager.GetUserAsync(User)).Id;
            model.TrangThai = 0;
            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = await _khambenhRep.SearchByCondition(model);

            ViewBag.Names = listPaged;
            ViewBag.Page = model.Page;
            ViewBag.Data = model;
            return View();
           
        }

        public async Task<IActionResult> KhamBenh(string MaPK)
        {
            var item = await _khambenhRep.GetPK(Guid.Parse(MaPK));
            item.NgayTaiKham = item.NgayKham.AddDays(7);
            ViewBag.LichSuKham = item.MaBNNavigation.PhieuKham.Where(x => x.TrangThai >=1&&x.ToaThuoc!=null).ToList()??new List<PhieuKham>();
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

        public async Task<IActionResult> GetJsonPK(string MaPK)
        {
            var item = await _khambenhRep.GetPK(Guid.Parse(MaPK));

            return Ok(item);
          
        }


        [Produces("application/json")]
        [HttpGet("search")]
        [Route("api/Benh/search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var benh = (await _tienichRep.SearchBenh(term)).Select(x=>x.TenBenh);
                return Ok(benh);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("searchtrieuchung")]
        [Route("api/Benh/searchtrieuchung")]
        public async Task<IActionResult> SearchTrieuChung()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var trieuchung = (await _tienichRep.GetTrieuChung(term)).Select(x => x.TenTrieuChung);
                return Ok(trieuchung);
            }
            catch
            {
                return BadRequest();
            }
        }



        [HttpPost]
        public async Task<IActionResult> ThemToa(PhieuKham model, List<string> ListTrieuChung)
        {
            foreach(var item in model.ToaThuoc.ChiTietToaThuoc)
            {
                item.DonGiaThuoc = (await _thuocRep.Get(item.MaThuoc)).DonGia;
                item.GhiChu = $"Ngày uống {item.LanTrongNgay} lần, mỗi lần {item.VienMoiLan},uống {(item.TruocKhian ? "trước khi ăn":"sau khi ăn")},Uống {(item.Sang ? "Sáng" : "")}{(item.Trua ? ", trưa" : "")}{(item.Chieu ? ", chieu" : "")}.";
            }    
            var result = await _khambenhRep.AddToaThuoc(model,ListTrieuChung);

            if (result != null)
            {


                await _hubContext.Clients.All.SendAsync("SendToaThuoc");
                return Json(new { status = 1, title = "", text = "Gửi thành công.", redirectUrL = Url.Action("PhieuKham", "BacSi"), obj = "" }, new JsonSerializerSettings());
            }

            else
                return Json(new { status = -2, title = "", text = "Gửi không thành công", obj = "" }, new JsonSerializerSettings());
        }


        public async Task<JsonResult> GetAutoFill(string TenBenh)
        {
            var item = await _tienichRep.GetAuToFill(TenBenh);

            return Json(JsonConvert.SerializeObject(item, Formatting.Indented,
new JsonSerializerSettings
{
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
}));


        }


        [HttpPost]
        public async Task<IActionResult> XacNhanKetQua(PhieuKham model,List<string> ListTrieuChung)
        {
            if (ListTrieuChung != null && ListTrieuChung.Count > 0)

            {
                foreach (var item in model.ToaThuoc.ChiTietToaThuoc)
                {
                    item.MaThuocNavigation = new Thuoc();
                    item.MaThuocNavigation = (await _thuocRep.Get(item.MaThuoc));
                }
                ViewBag.LisTTC = ListTrieuChung;


            return PartialView("_XacNhanKetQua", model);
            }
         
            else
                return Json(new { status = -2, title = "", text = "Vui lòng nhập ít nhất một triệu chứng", obj = "" }, new JsonSerializerSettings());

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
            model.TrangThai = 1;
            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = await _khambenhRep.SearchByCondition(model);

            ViewBag.Names = listPaged;
            ViewBag.Data = model;
            return View();
        }

        public async Task<IActionResult> PagePhieuKham(PhieuKhamSearchModel model)
        {
            
            model.MaBS = (await _userManager.GetUserAsync(User)).Id;
            model.TrangThai = 1;
            var listmodel = await _khambenhRep.SearchByCondition(model);
           
                if (!model.Page.HasValue) model.Page = 1;




                ViewBag.Names = listmodel;
                ViewBag.Data = model;

                return PartialView("_LichSuKham", listmodel);
                //return View("DanhSachThuoc", listmodel);
          
        }



        //public async Task<IActionResult> DanhSachThuoc(ThuocSearchModel model)
        //{
        //    if (!model.Page.HasValue) model.Page = 1;
        //    model.TrangThai = false;
        //    var listPaged = await _thuocRep.SearchByCondition(model);

        //    ViewBag.Names = listPaged;
        //    ViewBag.Data = model;
        //    return View(new ThuocSearchModel());
        //}

        //[HttpGet]
        //public async Task<IActionResult> PageList(ThuocSearchModel model)
        //{
        //    model.TrangThai = false;
        //    var listmodel = await _thuocRep.SearchByCondition(model);
        //    if (listmodel.Count() > 0)
        //    {

        //        if (!model.Page.HasValue) model.Page = 1;

        //        ViewBag.Names = listmodel;
        //        ViewBag.Data = model;

        //        return PartialView("_NameListThuoc", listmodel);
        //    }
        //    else
        //    {

        //        return Json(new { status = -2, title = "", text = "Không tìm thấy", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        //    }


        //}

        public async Task<IActionResult> ChiTietThuoc(Guid id)
        {
            if (await _thuocRep.Get(id) == null)
            {
                return NotFound(); ;
            }
            else
            {


                return PartialView("_ChiTietThuoc", await _thuocRep.Get(id));
            }
        }

        public IActionResult ThemThuoc()
        {
            return PartialView("_ThemThuoc");
        }



    }
}
