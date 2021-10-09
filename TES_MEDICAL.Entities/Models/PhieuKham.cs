using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.Entities.Models
{
    public partial class PhieuKham
    {
        public PhieuKham()
        {
            ChiTietDV = new HashSet<ChiTietDV>();
            ChiTietSinhHieu = new HashSet<ChiTietSinhHieu>();
            HoaDon = new HashSet<HoaDon>();
        }

        public Guid MaPK { get; set; }
        public Guid MaBS { get; set; }
        public Guid MaBN { get; set; }
        public string Mach { get; set; }
        public string NhietDo { get; set; }
        public string HuyetAp { get; set; }
        public string TrieuChung { get; set; }
        public string ChanDoan { get; set; }
        public DateTime NgayKham { get; set; }
        public DateTime NgayTaiKham { get; set; }

        public virtual BenhNhan MaBNNavigation { get; set; }
        public virtual NhanVienYte MaBSNavigation { get; set; }
        public virtual STTPhieuKham STTPhieuKham { get; set; }
        public virtual ToaThuoc ToaThuoc { get; set; }
        public virtual ICollection<ChiTietDV> ChiTietDV { get; set; }
        public virtual ICollection<ChiTietSinhHieu> ChiTietSinhHieu { get; set; }
        public virtual ICollection<HoaDon> HoaDon { get; set; }
    }
}
