using TES_MEDICAL.ADMIN.Server.Services;
using TES_MEDICAL.ADMIN.Shared;
using TES_MEDICAL.ADMIN.Shared.Models;
using TES_MEDICAL.ADMIN.Shared.SearchModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TES_MEDICAL.ADMIN.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class ChuyenKhoaController : ControllerBase
    {
        private readonly IChuyenKhoa _services;
        public ChuyenKhoaController(IChuyenKhoa services)
        {
            _services = services;
        }
        // GET: api/<ChuyenKhoaController>
       
        [HttpPost("getpage")]
        public async Task< IActionResult> Get(ChuyenKhoaApiSearchModel searchModel)
       {
            var ChuyenKhoas = await _services.Get(searchModel);
            Response.Headers.Add("X-PaginationChuyenKhoa", JsonConvert.SerializeObject(ChuyenKhoas.MetaData));
            return   Ok(ChuyenKhoas);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _services.Get());
        }    



        // GET api/<ChuyenKhoaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok( await _services.Get(id));
        }

        // POST api/<ChuyenKhoaController>
        [HttpPost]
        public async Task<IActionResult> Post(ChuyenKhoa model)
        {
            model.MaCK = Guid.NewGuid();
            var item = await _services.Add(model);
            if (item == null) return BadRequest();
            return Ok(item);
        }

        // PUT api/<ChuyenKhoaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(ChuyenKhoa model)
        {
            var item = await _services.Update(model);
            if (item == null) return BadRequest();
            return Ok(item);
        }

    }
}
