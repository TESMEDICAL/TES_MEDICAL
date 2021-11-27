using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class BenhNhan
    {
        public BenhNhan()
        {
            PhieuKham = new HashSet<PhieuKham>();
        }
        public Guid MaBN { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập Họ tên bệnh nhân")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        public string SDT { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn ngày sinh")]
        public DateTime? NgaySinh { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn giới tính")]
        public bool GioiTinh { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập địa chỉ")]
        public string DiaChi { get; set; }

        public virtual ICollection<PhieuKham> PhieuKham { get; set; }
    }
}
