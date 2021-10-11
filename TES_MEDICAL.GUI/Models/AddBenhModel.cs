using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Models
{
    public class AddBenhModel
    {
        public Guid MaBenh { get; set; }
        public string TenBenh { get; set; }
        public ICollection<TrieuChungModel> trieuChungs { get; set; }

    }
    public class TrieuChungModel
    {
        public string TenTrieuChung { get; set; }
        public string ChiTietTrieuChung { get; set; }

    }
}
