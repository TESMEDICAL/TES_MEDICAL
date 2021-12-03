using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Models.ViewModel
{
    public class PhieuKhamViewModel
    {
        public string MaPhieuDatLich { get; set; } 
           
        [Required(ErrorMessage ="Bạn cần nhập họ tên")]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        [RegularExpression(@"^\(?([0-9]{3})[-. ]?([0-9]{4})[-. ]?([0-9]{3})$", ErrorMessage = "Số điện thoại không đúng")]
        public string SDT { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Bạn cần chọn ngày sinh")]
        public DateTime? NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập địa chỉ")]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Bạn cần chọn bác sĩ")]
        public string MaBS { get; set; }
        public string TrieuChung { get; set; }
        public bool UuTien { get; set; }
        public List<DichVu> dichVus { get; set; }
        public string MaNVHD { get; set; }
    } 
}
