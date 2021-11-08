using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.ADMIN.Shared.Models
{
    public partial class PhanLoai
    {
        public PhanLoai()
        {
            Product = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public string TenLoai { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
