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

        //Hàm change STT Toa Thuốc
        public async Task<STTTOATHUOC> ChangeSoUuTien(Guid maPK)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    
                    var existingSTT = await _context.STTTOATHUOC.FindAsync(maPK);
                    existingSTT.UuTien = "C";
                    


                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return existingSTT;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        //Get All Toa Thuốc Có Trạng Thái
        public async Task<IEnumerable<ToaThuoc>> GetAllToaThuocCTT(byte TrangThai)
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

       
        public async Task<ToaThuoc> ThanhToanThuoc(Guid maPK)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var stt = await _context.STTTOATHUOC.FindAsync(maPK);
                    var existingThuoc = await _context.ToaThuoc.FindAsync(maPK);
                    existingThuoc.TrangThai = 1;
                    stt.UuTien = "B";
                    stt.STT =  _context.ToaThuoc.Include(x =>x.STTTOATHUOC).Where(x => x.TrangThai == 1).Max(x => x.STTTOATHUOC.STT);


                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return existingThuoc;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
