using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class CTTrieuChung
    {
        public Guid MaBenh { get; set; }
        public string TenTrieuChung { get; set; }
        public string ChiTietTrieuChung { get; set; }

        public virtual Benh MaBenhNavigation { get; set; }
        public virtual TrieuChung TenTrieuChungNavigation { get; set; }
    }
}
