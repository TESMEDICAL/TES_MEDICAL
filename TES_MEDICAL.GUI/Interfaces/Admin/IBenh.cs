using TES_MEDICAL.GUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using X.PagedList;
using TES_MEDICAL.ENTITIES.Models.ViewModel;

namespace TES_MEDICAL.GUI.Interfaces
{
    public interface IBenh
    {
        Task<Response<Benh>> Add(Benh model,List<CTrieuChungModel> TrieuChungs);
        Task<Benh> Get(Guid id);
        Task<Response<Benh>> Edit(Benh model,List<CTrieuChungModel> trieuchungs);
        Task<bool> Delete(Guid Id);
        Task<IPagedList<Benh>> SearchByCondition(BenhSearchModel model);
        Task<IEnumerable<ChuyenKhoa>> ChuyenKhoaNav();
    }
}



