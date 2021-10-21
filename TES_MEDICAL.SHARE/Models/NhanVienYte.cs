using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TES_MEDICAL.GUI.Models
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

        [Required(ErrorMessage = "Bạn cần nhập Email")]
        public string EmailNV { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("MatKhau", ErrorMessage = "Mật khẩu không khớp")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập họ tên")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        public string SDTNV { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn chức vụ")]
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
