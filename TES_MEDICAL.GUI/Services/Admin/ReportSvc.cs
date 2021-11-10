using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Services
{
    public class ReportSvc : IReport
    {
        private readonly DataContext _context;
        public ReportSvc(DataContext context)
        {
            _context = context;

        }

        public async Task<HoaDon> Get(string MaHD)
        {
            var item = await _context.HoaDon.Include(x => x.MaNVNavigation).Include(x => x.MaPKNavigation).ThenInclude(x => x.MaBSNavigation).Include(x => x.MaPKNavigation).ThenInclude(x => x.MaBNNavigation).Include(x => x.ChiTietDV).ThenInclude(x => x.MaDVNavigation)

                .FirstOrDefaultAsync(i => i.MaHoaDon == MaHD);


            if (item == null)
            {
                return null;
            }
            return item;
        }

        public async Task<IEnumerable<HoaDon>> GetAllHoaDon()
        {
            return await _context.HoaDon.Include(x => x.MaNVNavigation).Include(x => x.MaPKNavigation).ThenInclude(x => x.MaBSNavigation).ToListAsync();
        }

        public async Task<IEnumerable<HoaDonThuoc>> GetAllHoaDonThuoc()
        {
            return await _context.HoaDonThuoc.Include(x => x.MaNVNavigation).Include(x => x.MaPKNavigation).ThenInclude(x => x.MaPhieuKhamNavigation).ThenInclude(x => x.MaBSNavigation).ToListAsync();
        }

        public async Task<HoaDonThuoc> GetTTHDThuoc(string MaHD)
        {
            var item = await _context.HoaDonThuoc.Include(x => x.MaNVNavigation).Include(x => x.MaPKNavigation).ThenInclude(x =>x.MaPhieuKhamNavigation).ThenInclude(x => x.MaBSNavigation).Include(x =>x.MaPKNavigation).ThenInclude(x => x.MaPhieuKhamNavigation).ThenInclude(x => x.MaBNNavigation).Include(x =>x.MaPKNavigation).ThenInclude(x => x.ChiTietToaThuoc).ThenInclude(x=>x.MaThuocNavigation)

                 .FirstOrDefaultAsync(i => i.MaHD == MaHD);


            if (item == null)
            {
                return null;
            }
            return item;
        }
    }
}
