using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class PhieuDatLich
    {
        public string MaPhieu { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        public string SDT { get; set; }

       
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập họ và tên")]
        public string TenBN { get; set; }
        [DataType(DataType.Date,ErrorMessage ="Ngày không hợp lệ")]
        [Required(ErrorMessage = "Bạn cần chọn ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NgaySinh { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Ngày không hợp lệ")]
        [Required(ErrorMessage = "Bạn cần chọn ngày khám")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NgayKham { get; set; }
    }
}
