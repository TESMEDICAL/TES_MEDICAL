using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Infrastructure;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;

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
            catch(Exception ex)
            {
                
                return null;
            }
            
        }

        public async Task<PhieuDatLich> GetPhieuDat(string MaPhieu)
        {
            return await _context.PhieuDatLich.FindAsync(MaPhieu);
        }    
    }
}
