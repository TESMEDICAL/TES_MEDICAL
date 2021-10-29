using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.SHARE.Models.ViewModel;

namespace TES_MEDICAL.GUI.Interfaces
{
    public interface IKhamBenh
    {
        Task<IEnumerable<STTViewModel>> GetList(string MaBS);
        Task<PhieuKham> AddToaThuoc(PhieuKham model,bool UuTien);
        Task<PhieuKham> GetPK(Guid MaPK);
        Task<IEnumerable<Thuoc>> GetAllThuoc();
    }
}
