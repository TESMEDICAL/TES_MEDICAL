using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Helpers;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;


namespace TES_MEDICAL.GUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomer _service;
       
        public HomeController(ILogger<HomeController> logger, ICustomer service)
        {
            _logger = logger;
            _service = service;
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
            model.MaPhieu = "PK_" + Helper.GetUniqueKey();
            if (ModelState.IsValid)
            {
               
                if (await _service.DatLich(model) != null)
                {

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
    }
}
