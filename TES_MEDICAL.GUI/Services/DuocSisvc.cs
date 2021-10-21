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
            return await _context.ToaThuoc.Where(x =>x.TrangThai == TrangThai).ToListAsync();
        }
    }
}
