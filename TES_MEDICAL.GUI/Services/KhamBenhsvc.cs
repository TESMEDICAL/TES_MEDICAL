using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.GUI.Models.ViewModel;
using TES_MEDICAL.SHARE.Models.ViewModel;

namespace TES_MEDICAL.GUI.Services
{
    public class KhamBenhsvc : IKhamBenh
    {
        private readonly DataContext _context;
        public KhamBenhsvc(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<STTViewModel>> GetList(string MaBS)
        {
            return await (from x in _context.PhieuKham
                          join y in _context.STTPhieuKham

                          on x.MaPK equals y.MaPhieuKham
                          join bn in _context.BenhNhan
                          on x.MaBN equals bn.MaBN
                          where x.MaBS == MaBS
                          select new STTViewModel
                          {
                              STT = y.STT,
                              HoTen = bn.HoTen,
                              UuTien = y.MaUuTien,
                              MaPK = x.MaPK
                          }).OrderByDescending(x => x.UuTien).ThenBy(x => x.STT).ToListAsync();
        }

        public async Task<PhieuKham> GetPK(Guid MaPK)
        {
            return await _context.PhieuKham.Include(x=>x.MaBNNavigation).FirstOrDefaultAsync(x=>x.MaPK==MaPK);
        }

        

        public async Task<ToaThuoc> AddToaThuoc(ToaThuoc model,bool uutien)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    await _context.ToaThuoc.AddAsync(model);
                    var sttoathuoc = new STTTOATHUOC { MaPK = model.MaPhieuKham, STT = _context.STTTOATHUOC.Count() + 1, UuTien = uutien ? "A" : "B" };
                    await _context.STTTOATHUOC.AddAsync(sttoathuoc);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return model;
                }
                
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<ToaThuoc> GetToaThuoc(Guid MaPK)
        {
            return await _context.ToaThuoc.Include(x=>x.ChiTietToaThuoc).Include(x=>x.MaPhieuKhamNavigation).Include(x=>x.MaPhieuKhamNavigation.MaBNNavigation).FirstOrDefaultAsync(x=>x.MaPhieuKham==MaPK);
        }

        public async Task<ToaThuoc> AddToaThuoc(ToaThuoc model)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    await _context.ToaThuoc.AddAsync(model);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return model;
                }
                
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
