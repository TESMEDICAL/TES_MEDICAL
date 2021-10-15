using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class PhieuDatLich
    {
        public string MaPhieu { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string TenBN { get; set; }
        public DateTime? NgaySinh { get; set; }
        public DateTime? NgayKham { get; set; }
    }
}
