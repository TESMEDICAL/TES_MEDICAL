using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.Entities.Models
{
    public partial class BenhNhan
    {
        public BenhNhan()
        {
            PhieuKham = new HashSet<PhieuKham>();
        }

        public Guid MaBN { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string DiaChi { get; set; }

        public virtual ICollection<PhieuKham> PhieuKham { get; set; }
    }
}
