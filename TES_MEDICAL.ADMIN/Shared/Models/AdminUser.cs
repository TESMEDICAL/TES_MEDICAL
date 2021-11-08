using System;
using System.Collections.Generic;

#nullable disable

namespace TES_MEDICAL.ADMIN.Shared.Models
{
    public partial class AdminUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
        public byte Quyen { get; set; }
        public bool TrangThai { get; set; }
    }
}
