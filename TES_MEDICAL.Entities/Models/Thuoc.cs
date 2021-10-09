using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.Entities.Models
{
    public partial class Thuoc
    {
        public Thuoc()
        {
            ChiTietToaThuoc = new HashSet<ChiTietToaThuoc>();
        }

        public Guid MaThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string Vitri { get; set; }
        public decimal DonGia { get; set; }
        public string ThongTin { get; set; }
        public bool TrangThai { get; set; }
        public string HinhAnh { get; set; }

        public virtual ICollection<ChiTietToaThuoc> ChiTietToaThuoc { get; set; }
    }
}
