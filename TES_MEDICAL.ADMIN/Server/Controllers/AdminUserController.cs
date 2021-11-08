using TES_MEDICAL.ADMIN.Server.Services;
using TES_MEDICAL.ADMIN.Shared;
using TES_MEDICAL.ADMIN.Shared.Helper;
using TES_MEDICAL.ADMIN.Shared.Models;
using TES_MEDICAL.ADMIN.Shared.SearchModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminUser _services;
        public AdminUserController(IAdminUser services)
        {
            _services = services;
        }
        // GET: api/<AdminUserController>

        [HttpPost("getpage")]
        public async Task<IActionResult> Get(AdminUserSearchModel searchModel)
        {
            var AdminUsers = await _services.Get(searchModel);
            Response.Headers.Add("X-PaginationAdminUser", JsonConvert.SerializeObject(AdminUsers.MetaData));
            return Ok(AdminUsers);
        }





        // GET api/<AdminUserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _services.Get(id));
        }

        // POST api/<AdminUserController>
        [HttpPost]
        public async Task<IActionResult> Post(AdminUser model)
        {
            model.Pass = MaHoaHelper.Mahoa(model.Pass);
            model.Id = Guid.NewGuid();
            var item = await _services.Add(model);
            if (item == null) return BadRequest();
            return Ok(item);
        }

        // PUT api/<AdminUserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(AdminUser model)
        {
            var item = await _services.Update(model);
            if (item == null) return BadRequest();
            return Ok(item);
        }
        [HttpPatch]
        public async Task<bool> Patch(AdminUser model)
        {

            var item = await _services.Patch(model);
            if (item) return true;
            return false;

        }
    }


}
