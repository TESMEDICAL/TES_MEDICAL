using TES_MEDICAL.ADMIN.Server.Services;
using TES_MEDICAL.ADMIN.Shared.Models;
using TES_MEDICAL.ADMIN.Shared.SearchModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHang _services;
        public KhachHangController(IKhachHang services)
        {
            _services = services;
        }


        [HttpPost("getpage")]
        public async Task<IActionResult> Get(KhachHangSearchModel searchModel)
        {
            var KhachHangs = await _services.Get(searchModel);
            Response.Headers.Add("X-PaginationKhachHang", JsonConvert.SerializeObject(KhachHangs.MetaData));
            return Ok(KhachHangs);
        }

      






       


    }
}
