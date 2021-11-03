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
        public BacSiController (IKhamBenh khambenhRep)
        {
            _khambenhRep = khambenhRep;
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

        public IActionResult LichSuKham()
        {
            return PartialView("_LichSuKham");
        }
        [HttpPost]
        public IActionResult XacNhanKetQua(PhieuKham model)
        {
            return PartialView("_XacNhanKetQua");
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
