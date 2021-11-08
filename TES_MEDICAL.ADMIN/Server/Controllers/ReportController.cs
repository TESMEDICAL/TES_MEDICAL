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
    public class ReportController : ControllerBase
    {
        private readonly IDonHang _services;
        public ReportController(IDonHang services)
        {
            _services = services;
        }
     

        [HttpPost("getpage")]
        public async Task<IActionResult> Get(DonHangSearchModel searchModel)
        {
            var donHangs = await _services.Get(searchModel);
            Response.Headers.Add("X-PaginationDonHang", JsonConvert.SerializeObject(donHangs.MetaData));
            return Ok(donHangs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _services.Get(id));
        }



    


        // PUT api/<PhanLoaiController>/5
        [HttpPut]
        public async Task<IActionResult> Put(DonHang model)
        {
            var item = await _services.Update(model.MaDH, model.TrangThai);
            if (item == null) return BadRequest();
            return Ok(item);
        }


    }
}
