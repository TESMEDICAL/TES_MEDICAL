using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        
        private IHubContext<SignalServer> _hubContext;

        public HomeController(ILogger<HomeController> logger, ICustomer service, IValidate valid,  IHubContext<SignalServer> hubContext)
        {
            _logger = logger;
            _service = service;
            _valid = valid;
            _hubContext = hubContext;
           
        }

        public IActionResult Index()
        {
            
            return View();
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

                if (await _service.DatLich(model) != null)
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Có 1 lịch đặt mới");
                    Helper.SendMail("duongdoan.0904@gmail.com", "TES-MEDICAL", message(model));
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


        private string message(PhieuDatLich model)
        {
            var root = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot");
            using (var reader = new System.IO.StreamReader(root + @"/MailTheme/index.html"))
            {
                string readFile = reader.ReadToEnd();
                string StrContent = string.Empty;
                StrContent = readFile;
                //Assing the field values in the template
                StrContent = StrContent.Replace("{MaPhieu}", model.MaPhieu);
                return StrContent.ToString();
            }

        }
    }
}
