using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Models.ViewModel
{
    public class PhieuKhamViewModel
    {
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public DateTime? NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public Guid MaBS { get; set; }
        public string TrieuChung { get; set; }
        public bool UuTien { get; set; }
        public List<DichVu> dichVus { get; set; }
    }
  
}
