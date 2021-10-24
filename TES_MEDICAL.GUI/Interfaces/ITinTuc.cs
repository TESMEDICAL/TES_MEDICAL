

using TES_MEDICAL.GUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using X.PagedList;
namespace TES_MEDICAL.GUI.Interfaces
{
    public interface ITinTuc
    {
        Task<TinTuc> Add(TinTuc model);
        Task <TinTuc> Get(Guid id);
        Task <TinTuc> Edit(TinTuc model);
        Task <bool> Delete(Guid Id);
        Task <IPagedList<TinTuc>> SearchByCondition(TinTucSearchModel model);
        IEnumerable<NguoiDung>NguoiDungNav();
        
    }
}



