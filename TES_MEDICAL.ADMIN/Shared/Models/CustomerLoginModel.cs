using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.ADMIN.Shared.Models
{
    public class CustomerLoginModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
        public string Pass { get; set; }
    }
}
