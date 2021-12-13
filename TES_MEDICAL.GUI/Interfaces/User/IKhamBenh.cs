using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.SearchModel;
using TES_MEDICAL.ENTITIES.Models.ViewModel;
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.SHARE.Models.ViewModel;
using X.PagedList;

namespace TES_MEDICAL.GUI.Interfaces
{
    public interface IKhamBenh
    {
        
        Task<Response<PhieuKham>> AddToaThuoc(PhieuKham model,List<ChiTietBenhModel> ListCT);
        Task<PhieuKham> GetPK(Guid MaPK);
       
        Task<IEnumerable<PhieuKham>> GetLichSu(string Hoten, string SDT);
        Task<IPagedList<PhieuKham>> SearchByCondition(PhieuKhamSearchModel model);
        Task<STTPhieuKham> ChangeUuTien(Guid MaPK);
    }
}
