using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class TheLoai
    {
        public TheLoai()
        {
            TinTuc = new HashSet<TinTuc>();
        }

        public Guid MaTL { get; set; }
        public string TenTL { get; set; }

        public virtual ICollection<TinTuc> TinTuc { get; set; }
    }
}
