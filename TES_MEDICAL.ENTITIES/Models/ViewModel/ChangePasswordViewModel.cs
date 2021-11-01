using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.ENTITIES.Models.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu cũ")]
        public string CurrentPassword { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu xác nhận")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không đúng")]
        public string ConfirmPassword { get; set; }

    }
}
