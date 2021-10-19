using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Services
{
    public class PhieuDatLichSvc : ITiepNhan
    {
        private readonly DataContext _context;
        public PhieuDatLichSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<PhieuDatLich> Edit(PhieuDatLich model)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var existingLich = _context.PhieuDatLich.Find(model.MaPhieu);
                    existingLich.NgayKham = model.NgayKham;
                 

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return model;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<IEnumerable<PhieuDatLich>> GetAllPhieuDatLich()
        {
            return await _context.PhieuDatLich.ToListAsync();
        }

        public async Task<IEnumerable<ChuyenKhoa>> GetListCK()
        {
            return await _context.ChuyenKhoa.ToListAsync();
        }

        public async Task<PhieuDatLich> GetPhieuDatLichById(string id)
        {
            return await _context.PhieuDatLich.FindAsync(id);
        }
    }
}
