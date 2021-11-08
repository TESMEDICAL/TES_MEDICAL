using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Shared.SearchModel
{
    public class ProductSearchModel:PagingParameters
    {
        public ProductSearchModel()
        {
            PageSize = 4;
        }
        public string Name { get; set; }
        public int Gia { get; set; }
        public Guid MaLoai { get; set; }
        public bool TrangThai { get; set; }
    }
}
