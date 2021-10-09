using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.Entities.Models
{
    public partial class NguoiDung
    {
        public NguoiDung()
        {
            TinTuc = new HashSet<TinTuc>();
        }

        public Guid MaNguoiDung { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string HinhAnh { get; set; }
        public byte ChucVu { get; set; }
        public bool TrangThai { get; set; }

        public virtual ICollection<TinTuc> TinTuc { get; set; }
    }
}
