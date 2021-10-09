using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.Entities.Models
{
    public partial class HoaDonThuoc
    {
        public Guid MaPK { get; set; }
        public DateTime NgayHD { get; set; }
        public Guid MaNV { get; set; }
        public decimal TongTien { get; set; }

        public virtual NhanVienYte MaNVNavigation { get; set; }
        public virtual ToaThuoc MaPKNavigation { get; set; }
    }
}
