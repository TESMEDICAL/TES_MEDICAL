using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Constant;
using TES_MEDICAL.GUI.Filters;

namespace TES_MEDICAL.GUI.Controllers.Admin
{
    [AuthenticationFilter]
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        protected string GetUserName()
        {
            return HttpContext.Session.GetString(SessionKey.Nguoidung.UserName);
        }
        protected string GetFullName()
        {
            return HttpContext.Session.GetString(SessionKey.Nguoidung.FullName);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
