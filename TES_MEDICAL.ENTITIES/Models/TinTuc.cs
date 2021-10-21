using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class TinTuc
    {
        public Guid MaBaiViet { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tiêu đề")]
        public string TieuDe { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập nội dung")]
        public string NoiDung { get; set; }
        public bool TrangThai { get; set; }
        public Guid MaNguoiViet { get; set; }

        public virtual NguoiDung MaNguoiVietNavigation { get; set; }
    }
}
