

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using X.PagedList;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using Microsoft.AspNetCore.Identity;

namespace TES_MEDICAL.GUI.Services
{
    public class NhanVienYtesvc : INhanVienYte
    {
        private static int pageSize = 6;
        private readonly DataContext _context;
       

        public NhanVienYtesvc(DataContext context)
        {
            _context = context;
           

        }






      
        public async Task<NhanVienYte> Get(string id)
        {

            var item = await _context.NhanVienYte.Include(x => x.ChuyenKhoaNavigation)

                .FirstOrDefaultAsync(i => i.Id == id);


            if (item == null)
            {
                return null;
            }
            return item;


        }
       
        //         public  IEnumerable<ChuyenKhoa> ChuyenKhoaNav()
        //{
        //    return _context.ChuyenKhoa.ToList();
        //}



        public async Task<IPagedList<NhanVienYte>> SearchByCondition(NhanVienYteSearchModel model)
        {

            IEnumerable<NhanVienYte> listUnpaged;
            listUnpaged = _context.NhanVienYte.Include(x => x.ChuyenKhoaNavigation);



            if (!string.IsNullOrWhiteSpace(model.HoTenSearch))

            {
                listUnpaged = listUnpaged.Where(x => x.HoTen.ToUpper().Contains(model.HoTenSearch.ToUpper()));
            }


            if (!string.IsNullOrWhiteSpace(model.SDTNVSearch))

            {
                listUnpaged = listUnpaged.Where(x => x.PhoneNumber.ToUpper().Contains(model.SDTNVSearch.ToUpper()));
            }


            if (!string.IsNullOrWhiteSpace(model.ChuyenKhoaSearch.ToString()))

            {
                listUnpaged = listUnpaged.Where(x => x.ChuyenKhoa == model.ChuyenKhoaSearch);
            }









            var listPaged = await listUnpaged.ToPagedListAsync(model.Page ?? 1, pageSize);


            if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                return null;

            return listPaged;





        }


        public async Task<IEnumerable<NhanVienYte>> GetAllBS(Guid MaCK)
        {
            return await _context.NhanVienYte.Where(x => x.ChuyenKhoa == MaCK).ToListAsync();
        }
        protected IEnumerable<NhanVienYte> GetAllFromDatabase()
        {
            List<NhanVienYte> data = new List<NhanVienYte>();

            data = _context.NhanVienYte.ToList();


            return data;

        }
    }
}


