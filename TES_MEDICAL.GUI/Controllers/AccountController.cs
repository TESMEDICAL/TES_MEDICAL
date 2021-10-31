
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.GUI.Models.ViewModel;

namespace TES_MEDICAL.GUI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<NhanVienYte> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;

        public AccountController(UserManager<NhanVienYte> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
        }

        //[HttpPost("Registration")]
        //public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        //{
        //    if (userForRegistration == null || !ModelState.IsValid)
        //        return BadRequest();

        //    var user = new IdentityUser { UserName = userForRegistration.Email, Email = userForRegistration.Email };

        //    var result = await _userManager.CreateAsync(user, userForRegistration.Password);
        //    if (!result.Succeeded)
        //    {
        //        var errors = result.Errors.Select(e => e.Description);

        //        return BadRequest(new RegistrationResponseDto { Errors = errors });
        //    }

        //    return StatusCode(201);
        //}

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            string token = string.Empty;
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password)||user.ChucVu!=2)
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            token = GenerateJwtToken(user);
           
            
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }

        //private SigningCredentials GetSigningCredentials()
        //{
        //    var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
        //    var secret = new SymmetricSecurityKey(key);

        //    return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        //}

        //private List<Claim> GetClaims(IdentityUser user)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.Email)
        //    };

        //    return claims;
        //}

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {

            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings.GetSection("validIssuer").Value,
                audience: _jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
        protected string GenerateJwtToken(NhanVienYte user)
        {
            //getting the secret key
            var secretKey  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
           

            //create claims
            var claimEmail = new Claim(ClaimTypes.Email, user.Email);
            var claimName = new Claim(ClaimTypes.Name, user.HoTen);
            var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, user.Id);
           

            //create claimsIdentity
            var claimsIdentity = new ClaimsIdentity(new[] { claimEmail, claimNameIdentifier,claimName }, "serverAuth");

            // generate token that is valid for 7 days
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = claimsIdentity,
                Issuer= _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                
                Expires =  DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)
            };
            //creating a token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //returning the token back
            return tokenHandler.WriteToken(token);
        }

    }

}
