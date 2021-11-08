

using TES_MEDICAL.ADMIN.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using TES_MEDICAL.ADMIN.Shared.Helper;
using TES_MEDICAL.ADMIN.Shared.Models;
using TES_MEDICAL.ADMIN.Server.Helpers;

namespace TES_MEDICAL.ADMIN.Server.Controllers
{
    public interface ILoginAdmin
    {
        Task<AdminTokenData> Authenticate(string username, string password);
        //Task<IEnumerable<AdminUser>> GetAll();
        bool ValidateJwtToken(string token);
        


    }
    public class LoginAdminsvc:ILoginAdmin
    {
        private readonly DataContext _context;
        public IConfiguration _configuration;
        public LoginAdminsvc(DataContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        
            public async Task<AdminTokenData> Authenticate(string username, string password)
            {
                var user = await _context.NguoiDung.FirstOrDefaultAsync(x => x.Email == username && x.MatKhau ==MaHoaHelper.Mahoa(password));

                // return null if user not found
                if (user == null)
                    return null;

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.MaNguoiDung.ToString()),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var adminUser = new AdminTokenData();
            adminUser.FirstName = Helper.GetName(user.HoTen);
            adminUser.Token = tokenHandler.WriteToken(token);




            return adminUser;
            }





     
       

        public bool ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt: Key"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt: Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                

                // return account id from JWT token if validation successful
                return true;
            }
            catch
            {
                // return null if validation fails
                return false;
            }
        }

    }

}
