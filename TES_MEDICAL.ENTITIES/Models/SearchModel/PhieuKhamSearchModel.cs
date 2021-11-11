using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.ENTITIES.Models.SearchModel
{
    public class PhieuKhamSearchModel
    {   
            public int? Page { get; set; }
            public string MaBS { get; set; }
            public String KeywordSearch { get; set; }   
            public byte TrangThai { get; set; }
    }

   
}
