using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.ENTITIES.Models.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Bạn cần nhập email")]
        [DataType(DataType.Password)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
