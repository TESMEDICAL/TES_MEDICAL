﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class Thuoc
    {
        public Thuoc()
        {
            ChiTietToaThuoc = new HashSet<ChiTietToaThuoc>();
        }

        public Guid MaThuoc { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên thuốc")]
        public string TenThuoc { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập vị trí")]
        public string Vitri { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập đơn giá")]
        [RegularExpression(@"^(\d+),(\d{2})$", ErrorMessage = "Chỉ chấp nhận kiểu số dương")]
        public decimal? DonGia { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập thông tin")]
        public string ThongTin { get; set; }
        public bool TrangThai { get; set; }
        public string HinhAnh { get; set; }

        public virtual ICollection<ChiTietToaThuoc> ChiTietToaThuoc { get; set; }
    }
}
