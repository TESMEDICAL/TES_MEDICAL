using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class PhieuDatLich
    {
        public Guid MaPhieu { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập họ và tên")]
        public string TenBN { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn ngày sinh")]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn ngày khám")]
        public DateTime NgayKham { get; set; }
    }
}
