﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class STTPhieuKham
    {
        public Guid MaPhieuKham { get; set; }
        public int STT { get; set; }
        public string MaUuTien { get; set; }
        public bool TrangThai { get; set; }
        public virtual PhieuKham MaPhieuKhamNavigation { get; set; }
    }
}
