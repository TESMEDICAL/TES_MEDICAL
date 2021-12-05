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
        IEnumerable<Benh> SearchBenh(string KeyWord);
      
        Task<List<ChiTietToaThuoc>> GetToaThuocFill(List<string> TenBenh);
        IEnumerable<TrieuChung> GetTrieuChung(string TenTrieuChung);
        IEnumerable<Thuoc> GetAllThuoc();
        Thuoc GetThuoc(Guid MaThuoc);
        List<ListResponse> GetListChanDoan(List<string> ListTrieuChung);
        List<ResponseChanDoan> KetQuaChanDoan(List<string> ListTrieuChung);
     }
}
