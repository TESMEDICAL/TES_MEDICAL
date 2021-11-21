using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.GUI.Models
{
    public class ChiTietBenhPK
    {
        public Guid MaPK { get; set; }
        public Guid MaBenh { get; set; }
        
        public virtual PhieuKham MaPKNavigation { get; set; }
        public virtual Benh MaThuocNavigation { get; set; }
    }
}
