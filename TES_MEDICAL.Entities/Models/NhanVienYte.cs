using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.Entities.Models
{
    public partial class NhanVienYte
    {
        public NhanVienYte()
        {
            HoaDon = new HashSet<HoaDon>();
            HoaDonThuoc = new HashSet<HoaDonThuoc>();
            PhieuKham = new HashSet<PhieuKham>();
        }

        public Guid MaNV { get; set; }
        public string EmailNV { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string SDTNV { get; set; }
        public byte ChucVu { get; set; }
        public bool TrangThai { get; set; }
        public string Hinh { get; set; }
        public Guid? ChuyenKhoa { get; set; }

        public virtual ChuyenKhoa ChuyenKhoaNavigation { get; set; }
        public virtual ICollection<HoaDon> HoaDon { get; set; }
        public virtual ICollection<HoaDonThuoc> HoaDonThuoc { get; set; }
        public virtual ICollection<PhieuKham> PhieuKham { get; set; }
    }
}
