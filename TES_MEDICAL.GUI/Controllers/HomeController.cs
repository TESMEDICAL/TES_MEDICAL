using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TES_MEDICAL.GUI.Helpers;
using TES_MEDICAL.GUI.Infrastructure;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;


namespace TES_MEDICAL.GUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomer _service;
        private readonly IValidate _valid;
        private readonly ITinTuc _tintucService;
        private readonly IDuocSi _duocSiService;
        private readonly IDichVu _dichVuService;


        private IHubContext<SignalServer> _hubContext;

        public HomeController(ILogger<HomeController> logger, ICustomer service, IValidate valid, IHubContext<SignalServer> hubContext, ITinTuc tintucService, IDuocSi duocSiService,IDichVu dichvuService)
        {
            _logger = logger;
            _service = service;
            _valid = valid;
            _hubContext = hubContext;
            _tintucService = tintucService;
            _duocSiService = duocSiService;
            _dichVuService = dichvuService;

        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid MaTL)
        {
            ViewBag.TL1 = await _tintucService.GetTinTuc(Guid.Empty);
            ViewBag.TL2 = await _tintucService.GetTinTuc(Guid.Parse("7644AC01-B920-49C1-93C5-251319BBC90E"));
            ViewBag.TL3 = await _tintucService.GetTinTuc(Guid.Parse("AB6FE512-9C64-4EEA-BC14-25A517423C58"));
            ViewBag.TL4 = await _tintucService.GetTinTuc(Guid.Parse("AB215DC0-5855-42C3-85A5-EF00A2FABE65"));
            return View(await _tintucService.GetTinTuc(MaTL));
        }

        public IActionResult GioiThieu()
        {
            return View();
        }

        public IActionResult DatLich()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DatLich(PhieuDatLich model)
        {
            model.MaPhieu = "PK_" + (Helper.GetUniqueKey()).ToUpper();
            if (ModelState.IsValid)
            {
                if(model.NgayKham<DateTime.Now)
                {
                    ModelState.AddModelError("NgayKham", "Ngày khám phải sau ngày hiện tại");
                    return View(model);
                }    
                var result = await _service.DatLich(model);
                if (result != null)
                {

                  
                    Helper.SendMail(model.Email, "[TES-MEDICAL] Xác nhận đặt lịch khám", message(model)); //SendMail


                    await _hubContext.Clients.All.SendAsync("ReceiveMessage", result.TenBN, result.NgaySinh?.ToString("dd/MM/yyyy"), result.SDT, result.NgayKham, result.MaPhieu);

                    return RedirectToAction("ResultDatLich", "Home", new { MaPhieu = model.MaPhieu });
                }
            }


            return View(model);

        }



        public async Task<IActionResult> ResultDatLich(string MaPhieu)
        {
            var model = await _service.GetPhieuDat(MaPhieu);
            return View(model);
        }

        public IActionResult LichSuDatLich()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Nội dung mail
        private string message(PhieuDatLich model)
        {
            var request = HttpContext.Request;
            var _baseURL = $"{request.Scheme}://{request.Host}/Home/ResultDatLich?MaPhieu={model.MaPhieu}";
            var root = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot");
            using (var reader = new System.IO.StreamReader(root + @"/MailTheme/index.html"))
            {
                string readFile = reader.ReadToEnd();
                string StrContent = string.Empty;
                StrContent = readFile;
                //Assing the field values in the template
                StrContent = StrContent.Replace("{MaPhieu}", model.MaPhieu);
                StrContent = StrContent.Replace("{UrlResult}", _baseURL);
                //Url.Action("ResultDatLich", "Home", new { maPhieu = HttpUtility.UrlEncode(model.MaPhieu) }, _baseURL);
                return StrContent.ToString();
            }

        }
        //Partial View TinTuc Theo TheLoai
        public async Task<IActionResult> ListTheLoai(Guid MaTL)
        {

            return PartialView("_ListTheLoai", await _tintucService.GetTinTuc(MaTL));
        }

        public async Task<IActionResult> TinChiTiet(Guid id)
        {
            var baiViet = await _tintucService.Get(id);
            ViewBag.TL1 = await _tintucService.GetTinMin(Guid.Empty);

            ViewBag.Hinh = baiViet.Hinh;
            
            if (baiViet == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(baiViet);
        }

        public async Task<IActionResult> SearchByPhoneNumber(string SDT)
        {
            var listPhieuKham = await _service.SearchByPhoneNumber(SDT);
            if (listPhieuKham.Count() > 0)
            {

                return Json(JsonConvert.SerializeObject(listPhieuKham, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
            }
            else
            {

                return Json(new { status = -2, title = "", text = "Không tìm thấy", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }
        }

        public IActionResult LichSuKham()
        {
            return View();
        }

        public async Task<IActionResult> ChiTietLichSuKham(Guid MaPK)
        {
            ViewBag.CTLichSuDichVu = await _dichVuService.GetDichVu(MaPK);
            ViewBag.CTLichSuThuoc = await _duocSiService.GetChiTiet(MaPK);
            return PartialView("_PartialCT_LichSuKham", await _service.GetLichSuKhamById(MaPK));
        }
    }
}
