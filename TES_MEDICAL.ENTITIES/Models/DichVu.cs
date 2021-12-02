using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class DichVu
    {
        public DichVu()
        {
            ChiTietDV = new HashSet<ChiTietDV>();
        }
        public Guid MaDV { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên Dịch vụ")]
        public string TenDV { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập Đơn giá")]
        [Range(0, 9999999999.999999,ErrorMessage ="Đơn Giá không âm")]
        [RegularExpression(@"^(\d+),(\d{2})$", ErrorMessage = "Chỉ chấp nhận kiểu số dương")]
        public decimal? DonGia { get; set; }
        public bool TrangThai { get; set; }

        public virtual ICollection<ChiTietDV> ChiTietDV { get; set; }
    }
}
