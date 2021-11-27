

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Models
{
    public class TinTucSearchModel
    {   
        public int? Page  { get; set; }
        public String TieuDeSearch{ get; set; }
        public  bool 
        TrangThaiSearch{ get; set; }
        public  Guid? 
        MaTLSearch{ get; set; }
    }
}


