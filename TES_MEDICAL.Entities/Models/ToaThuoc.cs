using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.Entities.Models
{
    public partial class ToaThuoc
    {
        public ToaThuoc()
        {
            ChiTietToaThuoc = new HashSet<ChiTietToaThuoc>();
        }

        public Guid MaPhieuKham { get; set; }
        public byte TrangThai { get; set; }

        public virtual PhieuKham MaPhieuKhamNavigation { get; set; }
        public virtual HoaDonThuoc HoaDonThuoc { get; set; }
        public virtual ICollection<ChiTietToaThuoc> ChiTietToaThuoc { get; set; }
    }
}
