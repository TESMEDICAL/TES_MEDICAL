using System;
using System.Collections.Generic;
using System.Text;

namespace TES_MEDICAL.GUI.Models.ViewModel
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
