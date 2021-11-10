using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class Benh
    {
        public Benh()
        {
            CTTrieuChung = new HashSet<CTTrieuChung>();
        }

        public Guid MaBenh { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên bệnh")]
        public string TenBenh { get; set; }
        public string ThongTin { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn chuyên khoa")]
        public Guid MaCK { get; set; }

        public virtual ChuyenKhoa MaCKNavigation { get; set; }
        public virtual ICollection<CTTrieuChung> CTTrieuChung { get; set; }
        public virtual ICollection<PhieuKham> PhieuKham { get; set; }
    }
}
