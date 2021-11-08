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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TES_MEDICAL.ADMIN.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class PhanLoaiController : ControllerBase
    {
        private readonly IPhanLoai _services;
        public PhanLoaiController(IPhanLoai services)
        {
            _services = services;
        }
        // GET: api/<PhanLoaiController>
       
        [HttpPost("getpage")]
        public async Task <IActionResult> Get(PhanLoaiSearchModel searchModel)
        {
            var phanLoais = await _services.Get(searchModel);
            Response.Headers.Add("X-PaginationPhanLoai", JsonConvert.SerializeObject(phanLoais.MetaData));
            return   Ok(phanLoais);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _services.Get());
        }    



        // GET api/<PhanLoaiController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok( await _services.Get(id));
        }

        // POST api/<PhanLoaiController>
        [HttpPost]
        public async Task<IActionResult> Post(PhanLoai model)
        {
            model.Id = Guid.NewGuid();
            var item = await _services.Add(model);
            if (item == null) return BadRequest();
            return Ok(item);
        }

        // PUT api/<PhanLoaiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(PhanLoai model)
        {
            var item = await _services.Update(model);
            if (item == null) return BadRequest();
            return Ok(item);
        }

        // DELETE api/<PhanLoaiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(await _services.Delete(id))
            {
                return Ok(true);
              
            }
            return BadRequest();
        }
    }
}
