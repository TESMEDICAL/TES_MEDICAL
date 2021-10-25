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
using TES_MEDICAL.CLIENTBACSI.AuthProviders;
using TES_MEDICAL.CLIENTBACSI.HttpRepository;
using TES_MEDICAL.CLIENTBACSI.Services;

namespace TES_MEDICAL.CLIENTBACSI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44303/api/") });
            builder.Services

                        .AddScoped<IHttpService, HttpService>()

                         .AddScoped<IModal, ModalServices>()


                         .AddScoped<IKhamBenh, KhamBenhsvc>();

          

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            await builder.Build().RunAsync();
        }
    }
}
