﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class DichVu
    {
        public DichVu()
        {
            ChiTietDV = new HashSet<ChiTietDV>();
        }

        public Guid MaDV { get; set; }
        public string TenDV { get; set; }
        public decimal DonGia { get; set; }
        public bool TrangThai { get; set; }

        public virtual ICollection<ChiTietDV> ChiTietDV { get; set; }
    }
}
