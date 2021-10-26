using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.GUI.Models.ViewModel;
using TES_MEDICAL.SHARE.Models.ViewModel;

namespace TES_MEDICAL.GUI.Interfaces
{
    public interface ITiepNhan
    {
        Task<IEnumerable<PhieuDatLich>> GetAllPhieuDatLich();

        Task<PhieuDatLich> GetPhieuDatLichById(string id);

        Task<PhieuDatLich> Edit(PhieuDatLich model);
        Task<HoaDon> CreatePK(PhieuKhamViewModel model);
     



    }
}
