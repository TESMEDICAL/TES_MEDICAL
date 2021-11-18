using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class NhanVienYte:IdentityUser
    {
        public NhanVienYte()
        {
            HoaDon = new HashSet<HoaDon>();
            HoaDonThuoc = new HashSet<HoaDonThuoc>();
            PhieuKham = new HashSet<PhieuKham>();
        }
        [PersonalData]
        public string HoTen { get; set; }



        [PersonalData]
        [Required(ErrorMessage = "Bạn cần chọn chức vụ")]
        public byte ChucVu { get; set; }
        [PersonalData]
        public bool TrangThai { get; set; }
        public string Hinh { get; set; }
        [PersonalData]
        public Guid? ChuyenKhoa { get; set; }
       

        public virtual ChuyenKhoa ChuyenKhoaNavigation { get; set; }
        public virtual ICollection<HoaDon> HoaDon { get; set; }
        public virtual ICollection<HoaDonThuoc> HoaDonThuoc { get; set; }
        public virtual ICollection<PhieuKham> PhieuKham { get; set; }
    }
}
