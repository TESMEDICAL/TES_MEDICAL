using TES_MEDICAL.ADMIN.Server.Services;
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
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _service;
        public CustomerController(ICustomer service)
        {
            _service = service;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(CustomerLoginModel model)
        {
            var khtoken = await _service.KhAuthenticatec(model);
            if(khtoken!=null)
            {
                return Ok(khtoken);

            }
            return Unauthorized();
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(KhachHang model)
        {
            if(await _service.IsExist(model.Email))
            {
                return BadRequest(-1);
            } else
            {
                model.Id = Guid.NewGuid();
                model.MatKhau = MaHoaHelper.Mahoa(model.MatKhau);
                var kh = await _service.Register(model);
                if (kh != null)
                {
                    return Ok(1);

                }
            }    
          
            return BadRequest(0);
        } 


       
        
        [HttpPost("ThucDon")]
        public async Task<IActionResult> GetProducts(HomeSearchModel searchModel)
        {
            var Products = await _service.Get(searchModel);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(Products.MetaData));
            return Ok(Products);
        }

       
        [HttpGet("PhanLoai")]
        public async Task<IActionResult> GetPhanLoai()
        {
            var pl = await _service.GetPhanLoai();
            return Ok(pl);
        }
        [Authorize(Roles = "Customer")]
        [HttpPost("CheckOut")]
        public async Task<IActionResult> CheckOut(DonHang model)
        {
            var response = await _service.CheckOut(model);
            if (response != null)
                return Ok(model);
            return BadRequest();
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("GetInfor/{id}")]

        public async Task<IActionResult> GetUserInfor(Guid id)
        {
            var user = await _service.GetInfor(id);
            if (user != null) return Ok(user);
            return BadRequest();
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("GetCTDH/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var List = await _service.CTHD(id);
            if (List != null) return Ok(List);
            return BadRequest();
        }

        [HttpGet("thucdon/{id}")]
        public async Task<Product> GetTD(Guid id)
        {
            return await _service.GetTD(id);
        }
        [Authorize(Roles = "Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKH(KhachHang khachHang)
        {
            var kh = await _service.UpdateKH(khachHang);
            if (kh != null) return Ok(kh);
            return BadRequest();
        }
    }
}
