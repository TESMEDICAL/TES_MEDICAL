using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Services
{
    public class DuocSisvc : IDuocSi
    {
        private readonly DataContext _context;
        public DuocSisvc(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToaThuoc>> GetAllToaThuocCTT(int TrangThai)
        {
            return await _context.ToaThuoc.Include(x => x.STTTOATHUOC).Include(x => x.MaPhieuKhamNavigation).Include(x => x.MaPhieuKhamNavigation.MaBNNavigation)
                .Where(x => x.TrangThai == TrangThai).ToListAsync();
        }

        public async Task<IEnumerable<ChiTietToaThuoc>> GetChiTiet(Guid MaPhieu)
        {
            return await _context.ChiTietToaThuoc.Include(x => x.MaThuocNavigation)
                .Where(x=>x.MaPK == MaPhieu).ToListAsync();
        }

        public async Task<ToaThuoc> GetToaThuocByMaPhieu(Guid MaPhieu)
        {
            return await _context.ToaThuoc.Include(x => x.MaPhieuKhamNavigation).Include(x => x.MaPhieuKhamNavigation.MaBNNavigation).Include(x => x.ChiTietToaThuoc)
                .FirstOrDefaultAsync(x => x.MaPhieuKham == MaPhieu);
        }

    }
}
