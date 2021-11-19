using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.ENTITIES.Models.ViewModel;
using TES_MEDICAL.GUI.Models;

namespace TES_MEDICAL.GUI.Interfaces
{
     public interface ITienIch
    {
        Task<List<Benh>> SearchBenh(string KeyWord);
        Task<PhieuKham> GetAuToFill(string TenBenh);
        Task<List<TrieuChung>> GetTrieuChung(string TenTrieuChung);
        List<ListResponse> GetListChanDoan(List<string> ListTrieuChung);
        List<ResponseChanDoan> KetQuaChanDoan(List<string> ListTrieuChung);


    }
}
