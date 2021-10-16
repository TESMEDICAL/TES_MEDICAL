using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Controllers
{
    public class QuanLyNguoiDungController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
