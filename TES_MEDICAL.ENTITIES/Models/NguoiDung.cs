using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class NguoiDung
    {
        public NguoiDung()
        {
            TinTuc = new HashSet<TinTuc>();
        }

        public Guid MaNguoiDung { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập Email")]
        public string Email { get; set; }

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

        [RegularExpression(@"^\(?([0-9]{3})[-. ]?([0-9]{4})[-. ]?([0-9]{3})$", ErrorMessage = "Số điện thoại không đúng")]
        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        public string SDT { get; set; }

        public string HinhAnh { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn chức vụ")]
        public byte ChucVu { get; set; }
        public bool TrangThai { get; set; }

        public virtual ICollection<TinTuc> TinTuc { get; set; }
    }
}
