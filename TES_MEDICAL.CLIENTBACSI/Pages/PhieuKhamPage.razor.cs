using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TES_MEDICAL.CLIENTBACSI.Services;
using TES_MEDICAL.ENTITIES.Models.ViewModel;
using TES_MEDICAL.GUI.Models;

using TES_MEDICAL.SHARE.Models.ViewModel;

namespace TES_MEDICAL.CLIENTBACSI.Pages
{
    public partial class PhieuKhamPage
    {
        private string _authMessage;
        private string _userId;
        private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();

        private HubConnection hubConnection;

        [Inject]

        protected AuthenticationStateProvider authProvider { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Inject]
        public IModal modal { get; set; }
        [Inject]

        public NavigationManager navigationManager { get; set; }
        [Inject]
        public IConfiguration _configuration { get; set; }
        public bool IsConnected { get; set; } = false;

        [Inject]
        IKhamBenh khambenhRep { get; set; }
        public List<STTViewModel> PhieuKhams { get; set; } = new List<STTViewModel>();
        protected async override Task OnInitializedAsync()
        {
            string baseAddress = _configuration["BaseAddress"];
            await base.OnInitializedAsync();
            await GetClaimsPrincipalData();


            PhieuKhams = await khambenhRep.GetPhieuKham(_userId);
            await JSRuntime.InvokeVoidAsync("TestDataTablesAdd", "#ListPK", PhieuKhams);


            hubConnection = new HubConnectionBuilder()
                                .WithUrl(baseAddress + "PhieuKham")
                                .Build();

            hubConnection.On<string, STTViewModel>("SentDocTor", async  (id, stt) =>
            {
                if(id==_userId)
                {
                    await JSRuntime.InvokeVoidAsync("AddPhieuKham", stt);
                }    
                

                
                StateHasChanged();
            });

            await hubConnection.StartAsync();

            IsConnected =
       hubConnection.State == HubConnectionState.Connected;
        }


      
       

        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                await hubConnection.DisposeAsync();
            }
        }
        private async Task GetClaimsPrincipalData()
        {
            var authState = (await authProvider.GetAuthenticationStateAsync()).User;
            var user = authState.FindFirst(c => c.Type.Contains("nameidentifier"))?.Value;

            if (authState.Identity.IsAuthenticated)
            {
                _authMessage = $"{authState.Identity.Name} is authenticated.";
                _claims = authState.Claims;
                _userId = authState.FindFirst(c => c.Type.Contains("nameid"))?.Value;
            }
            else
            {
                _authMessage = "The user is NOT authenticated.";
            }
        }
    }
}

