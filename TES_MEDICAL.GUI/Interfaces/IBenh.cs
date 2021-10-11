

using TES_MEDICAL.GUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using X.PagedList;
namespace TES_MEDICAL.GUI.Interfaces
{
    public interface IBenh
    {
        Task<Benh> Add(Benh model);
        Task <Benh> Get(Guid id);
        Task <Benh> Edit(Benh model);
        Task <bool> Delete(Guid Id);
        Task <IPagedList<Benh>> SearchByCondition(BenhSearchModel model);
        Task<IEnumerable<ChuyenKhoa>>ChuyenKhoaNav();
        
    }
}



