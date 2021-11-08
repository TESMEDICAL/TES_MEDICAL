using TES_MEDICAL.ADMIN.Server.Services;
using TES_MEDICAL.ADMIN.Shared;
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
    public class ProductController : ControllerBase
    {
        private readonly IProduct _services;
        public ProductController(IProduct services)
        {
            _services = services;
        }
        // GET: api/<ProductController>

        [HttpPost("getpage")]
        public async Task<IActionResult> Get(ProductSearchModel searchModel)
        {
            var Products = await _services.Get(searchModel);
            Response.Headers.Add("X-PaginationProduct", JsonConvert.SerializeObject(Products.MetaData));
            return Ok(Products);
        }



       

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _services.Get(id));
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post(Product model)
        {
            
            model.Id = Guid.NewGuid();
            if (string.IsNullOrWhiteSpace(model.Hinh))
                model.Hinh = @"StaticFiles\Images\unchose.jpg";
            var item = await _services.Add(model);
            if (item == null) return BadRequest();
            return Ok(item);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Product model)
        {
            var item = await _services.Update(model);
            if (item == null) return BadRequest();
            return Ok(item);
        }
        [HttpPatch]
        public async Task<bool> Patch(Product model)
        {
            
            var item = await _services.Patch(model);
             if (item) return true;
            return false;
                
        }    

        
        
    }
}
