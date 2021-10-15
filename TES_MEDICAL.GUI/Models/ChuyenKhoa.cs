using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Helpers;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Services;

#nullable disable

namespace TES_MEDICAL.GUI.Models
{
    public partial class ChuyenKhoa: IValidatableObject
    {
        public ChuyenKhoa()
        {
            Benh = new HashSet<Benh>();
            NhanVienYte = new HashSet<NhanVienYte>();
        }

        public Guid MaCK { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên Chuyên Khoa")]
      
        public string TenCK { get; set; }

        public virtual ICollection<Benh> Benh { get; set; }
        public virtual ICollection<NhanVienYte> NhanVienYte { get; set; }
        public  IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();


            

            //get the required service injected
            var _service = validationContext.GetService(typeof(IValidate)) as IValidate;

            // Database call through service for validation
            var isExist =  _service.ExistsChuyenKhoa(TenCK);
            if (isExist)
            {
                errors.Add(new ValidationResult("Chuyên khoa đã tồn tại", new[] { nameof(TenCK) }));
            }
            

            return errors;
        }
    }
}
