using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class TinTuc
    {
        public Guid MaBaiViet { get; set; }
        public string Hinh { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tiêu đề")]
        public string TieuDe { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập nội dung bài viết")]
        public string NoiDung { get; set; }
        public bool TrangThai { get; set; }
        public Guid MaNguoiViet { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn thể loại")]
        public Guid? MaTL { get; set; }
        public DateTime? ThoiGian { get; set; }


        public virtual NguoiDung MaNguoiVietNavigation { get; set; }
        public virtual TheLoai MaTLNavigation { get; set; }
    }
}
