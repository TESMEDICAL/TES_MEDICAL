using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TES_MEDICAL.ADMIN.Shared.Models
{
    public partial class Product
    {
        public Product()
        {
            CartDetail = new HashSet<CartDetail>();
        }

        public Guid Id { get; set; }
        public string TenMon { get; set; }
        public string MoTa { get; set; }
        public decimal Gia { get; set; }
        public string Hinh { get; set; }
        public bool TrangThai { get; set; }
        public Guid MaLoai { get; set; }
        public string QrURL { get; set; }
        [NotMapped]
        public string DetailUrl { get; set; }

        public virtual PhanLoai MaLoaiNavigation { get; set; }
        public virtual ICollection<CartDetail> CartDetail { get; set; }
    }
}
