using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL.ENTITIES.Models.ViewModel
{
    public class UpdateUser
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress]
        public string UpdateEmail { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập họ tên")]
        public string UpdateHoTen { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        [RegularExpression(@"^\(?([0-9]{3})[-. ]?([0-9]{4})[-. ]?([0-9]{3})$", ErrorMessage = "Số điện thoại không đúng")]
        public string UpdateSDT { get; set; }
        public string UpdateHinhAnh { get; set; }
       
    }
}
