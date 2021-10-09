using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.Entities.Models
{
    public partial class ChuyenKhoa
    {
        public ChuyenKhoa()
        {
            Benh = new HashSet<Benh>();
            NhanVienYte = new HashSet<NhanVienYte>();
        }

        public Guid MaCK { get; set; }
        public string TenCK { get; set; }

        public virtual ICollection<Benh> Benh { get; set; }
        public virtual ICollection<NhanVienYte> NhanVienYte { get; set; }
    }
}
