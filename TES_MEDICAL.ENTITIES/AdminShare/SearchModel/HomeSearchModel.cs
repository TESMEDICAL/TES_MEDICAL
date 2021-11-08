using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Shared.SearchModel
{
    public class HomeSearchModel:PagingParameters
    {
        public HomeSearchModel()
        {
            PageSize = 6;
        }
        public string Name { get; set; }
        public int Gia { get; set; }
        public Guid MaLoai { get; set; }
        public bool TrangThai { get; set; }
    }
}
