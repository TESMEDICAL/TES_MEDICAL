using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.SearchModel;
using TES_MEDICAL.GUI.Models;
using X.PagedList;

namespace TES_MEDICAL.GUI.Interfaces
{
    public interface IDuocSi
    {
        Task<IPagedList<ToaThuoc>> SearchToaThuoc(ToaThuocSearchModel model);
        Task<ToaThuoc> GetToaThuocByMaPhieu(Guid MaPhieu);
        Task <IEnumerable<ChiTietToaThuoc>> GetChiTiet(Guid MaPhieu);
        Task<STTTOATHUOC> ChangeSoUuTien(Guid maPK);
        Task<ToaThuoc> ThanhToanThuoc(Guid maPK,string MaNV);
        Task<ToaThuoc> XacNhanThuocDangCho(Guid maPK);
    }
}
