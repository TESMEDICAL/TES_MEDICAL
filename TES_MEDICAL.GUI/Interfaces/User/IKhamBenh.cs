using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.SearchModel;
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.SHARE.Models.ViewModel;
using X.PagedList;

namespace TES_MEDICAL.GUI.Interfaces
{
    public interface IKhamBenh
    {
        Task<IEnumerable<STTViewModel>> GetList(string MaBS);
        Task<PhieuKham> AddToaThuoc(PhieuKham model,List<string> TrieuChungs);
        Task<PhieuKham> GetPK(Guid MaPK);
        Task<IEnumerable<Thuoc>> GetAllThuoc();
        Task<IEnumerable<PhieuKham>> GetLichSu(string Hoten, DateTime NgaySinh);
      
        Task<IPagedList<PhieuKham>> SearchByCondition(PhieuKhamSearchModel model);

    }
}
