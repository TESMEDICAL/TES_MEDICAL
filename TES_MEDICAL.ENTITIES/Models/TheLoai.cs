using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class TheLoai
    {
        public TheLoai()
        {
            TinTuc = new HashSet<TinTuc>();
        }

        public Guid MaTL { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập thể loại")]
        public string TenTL { get; set; }

        public virtual ICollection<TinTuc> TinTuc { get; set; }
    }
}
