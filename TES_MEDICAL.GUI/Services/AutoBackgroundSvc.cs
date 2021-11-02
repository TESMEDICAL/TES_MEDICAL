using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Services
{
    public class AutoBackgroundSvc : IAutoBackground
    {
        private readonly DataContext _context;
        public AutoBackgroundSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AutoDelete()
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {

                    if (_context.STTPhieuKham.Count() > 0 || _context.STTTOATHUOC.Count() >0 )
                    {
                        var result = _context.Database.ExecuteSqlRaw("EXEC dbo.sp_DeleteIdPK_TT");
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;

            }
        }
    }
}
