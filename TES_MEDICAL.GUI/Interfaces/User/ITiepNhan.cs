using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.SearchModel;
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.GUI.Models.ViewModel;
using TES_MEDICAL.SHARE.Models.ViewModel;
using X.PagedList;

namespace TES_MEDICAL.GUI.Interfaces
{
    public interface ITiepNhan
    {
        Task<IPagedList<PhieuDatLich>> SearchByCondition(PhieuDatLichSearchModel model);
        Task<PhieuDatLich> GetPhieuDatLichById(string id);
        Task<PhieuDatLich> Edit(PhieuDatLich model);
        Task<HoaDon> CreatePK(PhieuKhamViewModel model);
        Task<BenhNhan> GetBN(string SDT);
        Task<IPagedList<PhieuKham>> GetListPhieuKham(PhieuKhamSearchModel model);
        Task<PhieuKham> GetPhieuKhamById(Guid id);
        Task<HoaDon> UpDateDichVu(string MaNV, Guid MaPK, List<ChiTietDV> chiTietDVs);

        Task<List<ChiTietDV>> GetListDVByPK(Guid MaPK);



    }
}
