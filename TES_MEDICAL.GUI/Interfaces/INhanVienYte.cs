

using TES_MEDICAL.GUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using X.PagedList;
namespace TES_MEDICAL.GUI.Interfaces
{
    public interface INhanVienYte
    {
        Task<NhanVienYte> Add(NhanVienYte model);
        Task <NhanVienYte> Get(Guid id);
        Task <NhanVienYte> Edit(NhanVienYte model);
        Task <bool> Delete(Guid Id);
        Task <IPagedList<NhanVienYte>> SearchByCondition(NhanVienYteSearchModel model);
        IEnumerable<ChuyenKhoa>ChuyenKhoaNav();
        
    }
}



