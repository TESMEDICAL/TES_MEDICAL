using TES_MEDICAL.ADMIN.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Client.Components
{
    public partial class PhanLoaiTable
    {
        [Parameter]
        public List<PhanLoai> Phanloais { get; set; }
    }
}
