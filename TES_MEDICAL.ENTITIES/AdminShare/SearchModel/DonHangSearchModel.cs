using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Shared.SearchModel
{
    public class DonHangSearchModel:PagingParameters
    {
        public DonHangSearchModel()
        {
            PageSize = 5;
        }
        public string Phone { get; set; }
        public byte TrangThai { get; set; }
    }
}
