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
        
        private IHubContext<SignalServer> _hubContext;

        public HomeController(ILogger<HomeController> logger, ICustomer service, IValidate valid, IHubContext<SignalServer> hubContext, ITinTuc tintucService)
        {
            _logger = logger;
            _service = service;
            _valid = valid;
            _hubContext = hubContext;
            _tintucService = tintucService;

        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid MaTL)
        {  
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
        public async Task<IActionResult> ListTheLoai(Guid MaTL)
        {

            return PartialView("_ListTheLoai", await _tintucService.GetTinTuc(MaTL));
        }

    }
}
