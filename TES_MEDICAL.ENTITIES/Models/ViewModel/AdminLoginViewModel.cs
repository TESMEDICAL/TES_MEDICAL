using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.ENTITIES.Models.ViewModel
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage ="Vui lòng nhập Email")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập mật khẩu")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
