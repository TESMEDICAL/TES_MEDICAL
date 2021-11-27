
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
//using TES_MEDICAL.GUI.Helpers;
//using TES_MEDICAL.GUI.Interfaces;
//using TES_MEDICAL.GUI.Services;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class ChuyenKhoa
    {
        public ChuyenKhoa()
        {
            Benh = new HashSet<Benh>();
            NhanVienYte = new HashSet<NhanVienYte>();
        }
        public Guid MaCK { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên Chuyên Khoa")]
      
        public string TenCK { get; set; }

        public virtual ICollection<Benh> Benh { get; set; }
        public virtual ICollection<NhanVienYte> NhanVienYte { get; set; }  
    }
}
