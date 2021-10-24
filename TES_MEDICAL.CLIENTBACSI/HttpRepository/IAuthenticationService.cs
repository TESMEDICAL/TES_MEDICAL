
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.GUI.Models.ViewModel;

namespace TES_MEDICAL.CLIENTBACSI.HttpRepository
{
    public interface IAuthenticationService
    {
      
        Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication); 
        Task Logout();
    }
}
