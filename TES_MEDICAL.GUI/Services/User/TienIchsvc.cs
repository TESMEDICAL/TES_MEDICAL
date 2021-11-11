using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Services

{
    public class TienIchsvc : ITienIch
    {
        private readonly DataContext _context;

        public TienIchsvc(DataContext context)
        {
            _context = context;

        }
        public async Task<List<Benh>> SearchBenh(string KeyWord)
        {
            return await _context.Benh.Where(x =>
           string.IsNullOrWhiteSpace(KeyWord) ||
           EF.Functions.Collate(x.TenBenh, "SQL_Latin1_General_Cp1_CI_AI").Contains(EF.Functions.Collate(KeyWord, "SQL_Latin1_General_Cp1_CI_AI"))
           )
               .OrderBy(x => x.TenBenh).ToListAsync();

        }
        public async Task<PhieuKham> GetAuToFill(string TenBenh)
        {
            return await (from pk in _context.PhieuKham.Include(x => x.MaBenhNavigation).Include(x => x.MaBNNavigation).ThenInclude(x => x.PhieuKham).Include(x => x.ToaThuoc).ThenInclude(x => x.ChiTietToaThuoc).ThenInclude(x => x.MaThuocNavigation)
                    join b in _context.Benh
                    on pk.MaBenh equals (b.MaBenh)
                    where b.TenBenh.Equals(TenBenh)
                    select pk).FirstOrDefaultAsync();
           
        }
       
    }
}
