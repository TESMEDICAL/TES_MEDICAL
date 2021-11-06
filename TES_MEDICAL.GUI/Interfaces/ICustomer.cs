using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Models;
using X.PagedList;

namespace TES_MEDICAL.GUI.Interfaces
{
    public interface ICustomer
    {
        Task<PhieuDatLich> DatLich(PhieuDatLich model);
        Task<PhieuDatLich> GetPhieuDat(string MaPhieu);

        Task<List<PhieuKham>> SearchByPhoneNumber(string SDT);

        Task<PhieuKham> GetLichSuKhamById(Guid MaPK);
    }
}
