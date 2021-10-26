

using TES_MEDICAL.GUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using X.PagedList;
namespace TES_MEDICAL.GUI.Interfaces
{
    public interface IChuyenKhoa
    {
        Task<ChuyenKhoa> Add(ChuyenKhoa model);
        Task<ChuyenKhoa> Get(Guid id);
        Task<ChuyenKhoa> Edit(ChuyenKhoa model);
        Task<bool> Delete(Guid Id);
        Task<IPagedList<ChuyenKhoa>> SearchByCondition(ChuyenKhoaSearchModel model);
        Task<IEnumerable<ChuyenKhoa>> GetAll();

    }
}



