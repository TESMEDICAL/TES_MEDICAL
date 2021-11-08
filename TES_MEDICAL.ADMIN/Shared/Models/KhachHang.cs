using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.ADMIN.Shared.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            DonHang = new HashSet<DonHang>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
     
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgaySinh { get; set; }
        public string MatKhau { get; set; }

        public virtual ICollection<DonHang> DonHang { get; set; }
    }
}
