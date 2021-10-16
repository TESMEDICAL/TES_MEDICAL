using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Interfaces
{
    public interface ITiepNhan
    {
        Task<IEnumerable<PhieuDatLich>> GetAllPhieuDatLich();

        Task<PhieuDatLich> GetPhieuDatLichById(string id);
    }
}
