
using TES_MEDICAL.ADMIN.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TES_MEDICAL.ADMIN.Shared.Models;

namespace TES_MEDICAL.ADMIN.Client.Services
{
    public interface IAuthenticationService
    {
        AdminTokenData User { get; set; }
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        //private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;

        public AdminTokenData User { get; set; }

        public AuthenticationService(
            HttpClient httpClient,
            //IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        )
        {
            _httpClient = httpClient;
            //_httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            
            User = await _localStorageService.GetItem<AdminTokenData>("user");

        }

        public async Task Login(string username, string password)
        {
            var itemJson = new StringContent(JsonSerializer.Serialize(new AdminLoginModel { UserName = username, Pass = password }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/token/authenticate", itemJson);
            var responseBody = await response.Content.ReadAsStreamAsync();

            User = await JsonSerializer.DeserializeAsync<AdminTokenData>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
          
            await _localStorageService.SetItem("user", User);
            
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("login");
        }
    }
}
