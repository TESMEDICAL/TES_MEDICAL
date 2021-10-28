using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Models.ViewModel
{
    public class PhieuKhamViewModel
    {
        [Required(ErrorMessage = "Bạn cần nhập tên bệnh nhân")]
        public string HoTen { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})[-. ]?([0-9]{4})[-. ]?([0-9]{3})$", ErrorMessage = "Số điện thoại không đúng")]
        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        public string SDT { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Ngày không hợp lệ")]
        [Required(ErrorMessage = "Bạn cần chọn ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NgaySinh { get; set; }
        [Required(ErrorMessage = "Bạn cần chọn giới tính")]
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
