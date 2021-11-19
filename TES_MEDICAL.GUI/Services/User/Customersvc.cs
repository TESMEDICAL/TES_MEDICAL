using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Infrastructure;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using X.PagedList;

namespace TES_MEDICAL.GUI.Services
{
    public class Customersvc : ICustomer
    {
        private readonly DataContext _context;
        private IHubContext<SignalServer> _hubContext;
        public Customersvc(DataContext context, IHubContext<SignalServer> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        public async Task<PhieuDatLich> DatLich(PhieuDatLich model)
        {
            try
            {
                await _context.PhieuDatLich.AddAsync(model);
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("displayNotification", "");
                return model;
            }
            catch
            {
                
                return null;
            }
            
        }

        public async Task<PhieuKham> GetLichSuKhamById(Guid MaPK)
        {
            return await _context.PhieuKham
                .Include(x => x.MaBNNavigation)
                .Include(x => x.ToaThuoc)
                .ThenInclude(x => x.ChiTietToaThuoc)
                .Include(x => x.HoaDon).ThenInclude(x=>x.ChiTietDV)
                .ThenInclude(x => x.MaDVNavigation)
                .FirstOrDefaultAsync(x => x.MaPK == MaPK);
        }

        public async Task<PhieuDatLich> GetPhieuDat(string MaPhieu)
        {
            return await _context.PhieuDatLich.FindAsync(MaPhieu);
        }

        public async Task<List<PhieuKham>> SearchByPhoneNumber(string SDT)
        {
            return await _context.PhieuKham.Include(x => x.MaBNNavigation).Where(x => x.MaBNNavigation.SDT == SDT).ToListAsync();

        }

        public async Task<List<PhieuDatLich>> SearchDatLichByPhonenumber(string SDT)
        {
            return await _context.PhieuDatLich.Where(x => x.NgayKham > DateTime.Now).Where(x => x.SDT == SDT).ToListAsync();

        }
    }
}
