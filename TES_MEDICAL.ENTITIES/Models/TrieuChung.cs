using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class TrieuChung
    {
        public TrieuChung()
        {
            CTTrieuChung = new HashSet<CTTrieuChung>();
        }

        public Guid MatrieuChung { get; set; }
        public string TenTrieuChung { get; set; }
       

        public virtual ICollection<CTTrieuChung> CTTrieuChung { get; set; }
    }
}
