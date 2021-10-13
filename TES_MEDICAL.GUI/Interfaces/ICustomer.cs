using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Interfaces
{
    public interface ICustomer
    {
        Task<PhieuDatLich> DatLich(PhieuDatLich model);
        Task<PhieuDatLich> GetPhieuDat(string MaPhieu);
    }
}
