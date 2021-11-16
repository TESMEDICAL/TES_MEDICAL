using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Controllers.Admin
{
    public class ErrorController : Controller
    {
        public IActionResult Error400()
        {
            return View();
        }
    }
}
