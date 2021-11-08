using TES_MEDICAL.ADMIN.Server.Models;
using TES_MEDICAL.ADMIN.Server.Paging;
using TES_MEDICAL.ADMIN.Shared.Models;
using TES_MEDICAL.ADMIN.Shared.SearchModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Server.Services
{
    public interface IKhachHang
    {
        Task<IEnumerable<KhachHang>> Get();
        Task<PagedList<KhachHang>> Get(KhachHangSearchModel searchModel);
   

    


    }
    public class KhachHangsvc : IKhachHang
    {
        private readonly DataContext _context;
        public KhachHangsvc(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<KhachHang>> Get()
        {
            return await _context.KhachHang.ToListAsync();
        }
        public async Task<PagedList<KhachHang>> Get(KhachHangSearchModel searchModel)
        {
            IEnumerable<KhachHang> listUnpaged;
            listUnpaged = await _context.KhachHang.ToListAsync();

            if (!string.IsNullOrWhiteSpace(searchModel.Phone))

            {
                listUnpaged = listUnpaged.Where(x => x.Phone.ToUpper().Contains(searchModel.Phone.ToUpper()));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Name))

            {
                listUnpaged = listUnpaged.Where(x => x.Phone.ToUpper().Contains(searchModel.Phone.ToUpper()));
            }



            return PagedList<KhachHang>
                .ToPagedList(listUnpaged, searchModel.PageNumber, searchModel.PageSize);
        }

      
      
    }
}
