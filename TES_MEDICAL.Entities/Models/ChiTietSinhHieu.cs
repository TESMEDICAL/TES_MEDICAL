using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.Entities.Models
{
    public partial class ChiTietSinhHieu
    {
        public Guid MaSinhHieu { get; set; }
        public Guid MaPK { get; set; }
        public string TenSH { get; set; }
        public string ThongTinChiTiet { get; set; }

        public virtual PhieuKham MaPKNavigation { get; set; }
    }
}
