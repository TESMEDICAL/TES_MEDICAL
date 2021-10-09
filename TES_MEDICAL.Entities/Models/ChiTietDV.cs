using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.Entities.Models
{
    public partial class ChiTietDV
    {
        public Guid MaPhieuKham { get; set; }
        public Guid MaDV { get; set; }

        public virtual DichVu MaDVNavigation { get; set; }
        public virtual PhieuKham MaPhieuKhamNavigation { get; set; }
    }
}
