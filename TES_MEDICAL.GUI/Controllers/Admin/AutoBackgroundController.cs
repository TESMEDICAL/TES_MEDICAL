using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Controllers.Admin;
using TES_MEDICAL.GUI.Interfaces;

namespace TES_MEDICAL.GUI.Controllers
{
    public class AutoBackgroundController : BaseController
    {
        private readonly IAutoBackground _service;
        public AutoBackgroundController(IAutoBackground service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View(_service.AutoDelete());
        }
    }
}
