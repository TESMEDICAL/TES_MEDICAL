using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.SHARE.Models.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TES_MEDICAL.GUI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiKhamBenhController : ControllerBase
    {
        private readonly IKhamBenh _sevices;
        public ApiKhamBenhController(IKhamBenh sevices)
        {
            _sevices = sevices;
        }
        // GET: api/<ApiKhamBenhController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ApiKhamBenhController>/5
        [HttpGet("GetPK")]
        public async Task<IActionResult> GetPK(string MaPK)
        {
            var item = await _sevices.GetPK(Guid.Parse(MaPK));
            item.NgayTaiKham = item.NgayKham.AddDays(7);
            return Ok(item);
        }
        [HttpGet("GetAllThuoc")]
        public async Task<IActionResult> GetAllThuoc()
        {
            return Ok(await _sevices.GetAllThuoc());
        }

        [HttpGet("GetListPK")]
        public async Task<IActionResult> GetListPK(string MaBS)
        {
            return Ok (await _sevices.GetList(MaBS));
        }

        [HttpPost("ThemToa")]
        public async Task<IActionResult> ThemToa(PhieuKham model)
        {
            return Ok(await _sevices.AddToaThuoc(model));
        }

        [HttpGet("GetLichSu")]

        public async Task<IActionResult> GetLichSu(string MaBN)
        {
            return Ok((await _sevices.GetLichSu(Guid.Parse(MaBN)))??new List<PhieuKham>());

        }

        // POST api/<ApiKhamBenhController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ApiKhamBenhController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApiKhamBenhController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
