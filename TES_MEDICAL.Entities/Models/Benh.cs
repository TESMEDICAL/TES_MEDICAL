using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.Entities.Models
{
    public partial class Benh
    {
        public Benh()
        {
            CTTrieuChung = new HashSet<CTTrieuChung>();
        }

        public Guid MaBenh { get; set; }
        public string TenBenh { get; set; }
        public string ThongTin { get; set; }
        public Guid MaCK { get; set; }

        public virtual ChuyenKhoa MaCKNavigation { get; set; }
        public virtual ICollection<CTTrieuChung> CTTrieuChung { get; set; }
    }
}
