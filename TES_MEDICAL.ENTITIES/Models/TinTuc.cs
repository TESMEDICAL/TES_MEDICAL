using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class TinTuc
    {
        public Guid MaBaiViet { get; set; }
        public string Hinh { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public bool TrangThai { get; set; }
        public Guid MaNguoiViet { get; set; }
        public Guid? MaTL { get; set; }

        public virtual NguoiDung MaNguoiVietNavigation { get; set; }
        public virtual TheLoai MaTLNavigation { get; set; }
    }
}
