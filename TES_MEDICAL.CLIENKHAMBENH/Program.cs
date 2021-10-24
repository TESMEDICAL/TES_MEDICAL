using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TES_MEDICAL.CLIENKHAMBENH.Services;

namespace TES_MEDICAL.CLIENKHAMBENH
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services
                          
                         .AddScoped<IHttpService, HttpService>()

                          .AddScoped<IModal, ModalServices>()
                          .AddScoped<ILocalStorageServiceCookie, LocalStorageServices>()
                          
                          .AddScoped<IKhamBenh, KhamBenhsvc>();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44303/") });
            var host = builder.Build();

            //var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
            //await authenticationService.Initialize();

            await host.RunAsync();
        }
    }
}
