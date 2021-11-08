using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.ADMIN.Shared.Models
{
    public partial class CartDetail
    {
        public Guid MaDH { get; set; }
        public Guid MaTD { get; set; }
        public int SoLuong { get; set; }

        public virtual DonHang MaDHNavigation { get; set; }
        public virtual Product MaTDNavigation { get; set; }
    }
}
