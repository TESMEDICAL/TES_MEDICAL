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

    public interface IDonHang
    {
        Task<IEnumerable<DonHang>> Get();
        Task<PagedList<DonHang>> Get(DonHangSearchModel searchModel);
        Task<List<CartDetail>> Get(Guid id);
        
        Task<DonHang> Update(Guid id,byte TrangThai);
    

    }
    public class DonHangsvc : IDonHang
    {
        private readonly DataContext _context;
        public DonHangsvc(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DonHang>> Get()
        {
            return await _context.DonHang.ToListAsync();
        }
        public async Task<PagedList<DonHang>> Get(DonHangSearchModel searchModel)
        {
            IEnumerable<DonHang> listUnpaged;
            listUnpaged = await _context.DonHang.Include(x=>x.MaKHNavigation).OrderBy(x=>x.ThoiGian).ToListAsync();

            if (!string.IsNullOrWhiteSpace(searchModel.Phone))

            {
                listUnpaged = listUnpaged.Where(x => x.Phone.ToUpper().Contains(searchModel.Phone.ToUpper()));
            }
            if (searchModel.TrangThai != 0 ) listUnpaged = listUnpaged.Where(x => x.TrangThai == searchModel.TrangThai);


            return PagedList<DonHang>
                .ToPagedList(listUnpaged, searchModel.PageNumber, searchModel.PageSize);
        }

        public async Task<List<CartDetail>> Get(Guid id)
        {
            return await _context.CartDetail.Where(d=>d.MaDH==id).Include(d=>d.MaDHNavigation).Include(d=>d.MaTDNavigation).ToListAsync();
        }
       
        public async Task<DonHang> Update(Guid id, byte TrangThai)
        {
            try
            {
                var item = _context.DonHang.Find(id);
                item.TrangThai = TrangThai;
                _context.Update(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {

                return null;
            }

        }
      
    }
}
