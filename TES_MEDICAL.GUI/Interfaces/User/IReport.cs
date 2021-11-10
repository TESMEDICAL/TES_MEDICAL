using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Interfaces
{
    public interface IReport
    {
        Task<IEnumerable<HoaDon>> GetAllHoaDon();
        Task<HoaDon> Get(string MaHD);

        Task<IEnumerable<HoaDonThuoc>> GetAllHoaDonThuoc();
        Task<HoaDonThuoc> GetTTHDThuoc(string MaHD);  


    }
}
