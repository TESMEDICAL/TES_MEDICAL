

using TES_MEDICAL.GUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using X.PagedList;
namespace TES_MEDICAL.GUI.Interfaces
{
    public interface INguoiDung
    {
        Task<NguoiDung> Add(NguoiDung model);
        Task <NguoiDung> Get(Guid id);
        Task <NguoiDung> Edit(NguoiDung model);
        Task <bool> Delete(Guid Id);
        Task <IPagedList<NguoiDung>> SearchByCondition(NguoiDungSearchModel model);
        
    }
}



