using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Interfaces
{
    public interface IValidate
    {
        Task<bool> ExistsChuyenKhoa(string ten);
        bool CheckNgayKham(DateTime? ngay);
    }
}
