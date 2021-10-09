using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.Entities.Models
{
    public partial class ChiTietToaThuoc
    {
        public Guid MaPK { get; set; }
        public Guid MaThuoc { get; set; }
        public int SoLuong { get; set; }
        public string GhiChu { get; set; }

        public virtual ToaThuoc MaPKNavigation { get; set; }
        public virtual Thuoc MaThuocNavigation { get; set; }
    }
}
