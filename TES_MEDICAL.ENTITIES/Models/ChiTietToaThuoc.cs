using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class ChiTietToaThuoc
    {
        public Guid MaPK { get; set; }
        public Guid MaThuoc { get; set; }
        public int SoLuong { get; set; }
        public decimal? DonGiaThuoc { get; set; }
        public int LanTrongNgay { get; set; }
        public int VienMoiLan { get; set; }
        public bool TruocKhian { get; set; }
        public bool Sang { get; set; }
        public bool Trua { get; set; }
        public bool Chieu { get; set; }
        public string GhiChu { get; set; }

        public virtual ToaThuoc MaPKNavigation { get; set; }
        public virtual Thuoc MaThuocNavigation { get; set; }
    }
}
