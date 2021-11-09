

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
       
        Task <NhanVienYte> Get(string id);
     
        Task <IPagedList<NhanVienYte>> SearchByCondition(NhanVienYteSearchModel model);
        
        Task<IEnumerable<NhanVienYte>> GetAllBS(Guid MaCK);
        Task<Response<NhanVienYte>> Edit(string id, bool TrangThai);

    }
}



