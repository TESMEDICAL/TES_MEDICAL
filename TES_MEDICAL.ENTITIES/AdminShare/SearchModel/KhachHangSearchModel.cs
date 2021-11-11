using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Shared.SearchModel
{
    public class KhachHangSearchModel:PagingParameters
    {
        public string Phone { get; set; }
        public string Name { get; set; }
    }
}
