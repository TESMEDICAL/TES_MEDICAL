using TES_MEDICAL.ADMIN.Server.Models;
using TES_MEDICAL.ADMIN.Server.Paging;
using TES_MEDICAL.ADMIN.Shared.Helper;
using TES_MEDICAL.ADMIN.Shared.Models;
using TES_MEDICAL.ADMIN.Shared.SearchModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Server.Services
{
    public interface ICustomer
    {
        Task<CustomerToken> KhAuthenticatec(CustomerLoginModel model);
        Task<KhachHang> Register(KhachHang model);
        Task<PagedList<Product>> Get(HomeSearchModel model);
        Task<IEnumerable<PhanLoai>> GetPhanLoai();
        Task<DonHang> CheckOut(DonHang model);
        Task<KhachHang> GetInfor(Guid id);
        Task<List<CartDetail>> CTHD(Guid MaDH);
        Task<KhachHang> UpdateKH(KhachHang model);
        Task<Product> GetTD(Guid id);
        Task<bool> IsExist(string email);
        //Task<KhachHang> Get(Guid id);
        //Task<KhachHang> Edit(KhachHang model);

    }
    public class Customersvc:ICustomer
    {

        private readonly DataContext _context;
        public IConfiguration _configuration;
        public Customersvc(DataContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<CustomerToken> KhAuthenticatec(CustomerLoginModel model)
        {
            var kh = await _context.KhachHang.FirstOrDefaultAsync(x => x.Email == model.Email && x.MatKhau == MaHoaHelper.Mahoa(model.Pass));

            // return null if user not found
            if (kh == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, kh.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Customer")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokendata = new CustomerToken();

            tokendata.Khach = kh;
            tokendata.Khach.MatKhau = null;

            tokendata.Token = tokenHandler.WriteToken(token);
            return tokendata;
        }

        public async Task<KhachHang> Register(KhachHang khachHang)
        {
            try
            {
                _context.Entry(khachHang).State = EntityState.Added;
                await _context.SaveChangesAsync();
                return khachHang;
            } catch(Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> IsExist(string email)
        {
            return await _context.KhachHang.AnyAsync(x => x.Email == email);
         }

        public async Task<PagedList<Product>> Get(HomeSearchModel model)
        {
            IEnumerable<Product> listUnpaged;
            listUnpaged = await _context.Product.Include(t => t.MaLoaiNavigation).ToListAsync();

            if (!string.IsNullOrWhiteSpace(model.Name))

            {
                listUnpaged = listUnpaged.Where(x => x.TenMon.ToUpper().Contains(model.Name.ToUpper()));
            }




            if (model.Gia == 1) listUnpaged = listUnpaged.Where(x => x.Gia < 100000);
            if (model.Gia == 2) listUnpaged = listUnpaged.Where(x => x.Gia >= 100000 && x.Gia <= 300000);
            if (model.Gia == 3) listUnpaged = listUnpaged.Where(x => x.Gia > 300000);
            if (model.MaLoai != Guid.Empty) listUnpaged = listUnpaged.Where(x => x.MaLoai == model.MaLoai);
            if (model.TrangThai == false) listUnpaged = listUnpaged.Where(x => x.TrangThai == true);

            return PagedList<Product>
                .ToPagedList(listUnpaged, model.PageNumber, model.PageSize);
        }

     

        public async Task<IEnumerable<PhanLoai>> GetPhanLoai()
        {
            return await _context.PhanLoai.ToListAsync();
        }

        public async Task<DonHang> CheckOut(DonHang model)
        {
            try
            {
                _context.Add(model);
               await _context.SaveChangesAsync();
                return model;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
      public async Task<KhachHang> GetInfor(Guid id)
        {
            var user = await _context.KhachHang.Include(x=>x.DonHang).FirstOrDefaultAsync(x=>x.Id==id);
            return user;
        }
        public async Task<List<CartDetail>> CTHD(Guid MaDH)
        {
            try
            {
                return await _context.CartDetail.Include(x => x.MaTDNavigation).Where(x => x.MaDH == MaDH).ToListAsync();
            }
            catch
            {
                return null;
            }
            
        }


        public async Task<Product> GetTD(Guid id)
        {
            try
            {
                return await _context.Product.Include(x=>x.MaLoaiNavigation).FirstOrDefaultAsync(x=>x.Id==id);
                
            }
            catch
            {
                return null;
            }
        }

        public async Task<KhachHang> UpdateKH (KhachHang model)
        {
            try
            {
                var item = await _context.KhachHang.FindAsync(model.Id);
                item.Name = model.Name;
                item.Phone = model.Phone;
                item.DiaChi = model.DiaChi;
                _context.Update(item);
                await _context.SaveChangesAsync();
                return model;
            }    
            catch(Exception ex)
            {
                return null;
            }
          
        }

       



    }
}
