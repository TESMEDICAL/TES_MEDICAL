using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.ENTITIES.Models.ViewModel
{
    public partial class ListResponse
    {
        public string Result { get; set; }
       
    }

    public partial class ResponseChanDoan
    {
        public Guid MaBenh { get; set; }
        public string TenBenh { get; set; }
        public int SoTrieuChung { get; set; }
        public int TongCong { get; set; }
    }
}
