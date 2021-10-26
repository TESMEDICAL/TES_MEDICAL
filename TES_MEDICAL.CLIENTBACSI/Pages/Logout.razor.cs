using TES_MEDICAL.CLIENTBACSI.HttpRepository;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace TES_MEDICAL.CLIENTBACSI.Pages
{
    public partial class Logout
    {

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/");
        }
    }
}
