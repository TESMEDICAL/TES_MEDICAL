using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class HoaDon
    {
        public string MaHoaDon { get; set; }
        public Guid MaPK { get; set; }
        public string MaNV { get; set; }
        public DateTime NgayHD { get; set; }
        public decimal? TongTien { get; set; }

        public virtual NhanVienYte MaNVNavigation { get; set; }
        public virtual PhieuKham MaPKNavigation { get; set; }
    }
}
