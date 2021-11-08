using TES_MEDICAL.ADMIN.Shared;
using TES_MEDICAL.ADMIN.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TES_MEDICAL.ADMIN.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILoginAdmin _services;
      
        public TokenController(ILoginAdmin services)
        {
            _services = services;
        }
       
        
        // GET api/<TokenController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TokenController>
        [HttpPost("authenticate")]

        public async Task<IActionResult> Post(AdminLoginModel loginModel)
        {
            
            var tokendata = await _services.Authenticate(loginModel.UserName, loginModel.Pass);
            if (tokendata == null)
                return Unauthorized("Unauthorize");
            return  Ok(tokendata);
        }
      

       
    }
}
