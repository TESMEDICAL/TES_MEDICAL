using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class PhieuDatLich
    {
       public string MaPhieu { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})[-. ]?([0-9]{4})[-. ]?([0-9]{3})$", ErrorMessage = "Số điện thoại không đúng")]
        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        public string SDT { get; set; }


        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập họ và tên")]
        public string TenBN { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Ngày không hợp lệ")]
        [Required(ErrorMessage = "Bạn cần chọn ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NgaySinh { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Ngày không hợp lệ")]
        [Required(ErrorMessage = "Bạn cần chọn ngày khám")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]

        public DateTime? NgayKham { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NgayKham < DateTime.Now)
            {
                yield return new ValidationResult(
                    $"Ngày khám phải sau ngày hiện tại",
                    new[] { nameof(NgayKham) });
            }
        }
        
    }
}
