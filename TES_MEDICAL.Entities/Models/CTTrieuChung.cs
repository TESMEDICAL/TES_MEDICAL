using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.Entities.Models
{
    public partial class CTTrieuChung
    {
        public Guid MaBenh { get; set; }
        public Guid MaTrieuChung { get; set; }
        public string ChiTietTrieuChung { get; set; }

        public virtual Benh MaBenhNavigation { get; set; }
        public virtual TrieuChung MaTrieuChungNavigation { get; set; }
    }
}
