using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Interfaces
{
    public interface IDuocSi
    {
        Task<IEnumerable<ToaThuoc>> GetAllToaThuocCTT(int TrangThai);

        Task<ToaThuoc> GetToaThuocByMaPhieu(Guid MaPhieu);

        Task <IEnumerable<ChiTietToaThuoc>> GetChiTiet(Guid MaPhieu);

        Task<STTTOATHUOC> ChangeSoUuTien(Guid maPK);
    }
}
