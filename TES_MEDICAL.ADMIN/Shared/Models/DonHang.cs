using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.ADMIN.Shared.Models
{
    public partial class DonHang
    {
        public DonHang()
        {
            CartDetail = new HashSet<CartDetail>();
        }

        public Guid MaDH { get; set; }
        public DateTime ThoiGian { get; set; }
        public byte TrangThai { get; set; }
        public Guid MaKH { get; set; }
        public string Phone { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }

        public virtual KhachHang MaKHNavigation { get; set; }
        public virtual ICollection<CartDetail> CartDetail { get; set; }
    }
}
