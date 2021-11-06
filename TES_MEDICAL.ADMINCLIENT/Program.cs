
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;

namespace TES_MEDICAL.ADMINCLIENT
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services
                .AddScoped<IAuthenticationService, AuthenticationService>()
               .AddScoped<IHttpService, HttpService>()
            //    .AddScoped<IPhanLoaiHttpRepository, PhanLoaisvc>()
                .AddScoped<IModal, ModalServices>()
                .AddScoped<ILocalStorageService, LocalStorageService>();
            //    .AddScoped<IDonHangHttpRepository,DonHangsvc>()
            //    .AddScoped<IKhachHangHttpRepository,KhachHangsvc>()
            //    .AddScoped<IKhachHangHttpRepository,KhachHangsvc>();



            //builder.Services.AddScoped<IProductHttpRepository, Productsvc>();
            //builder.Services.AddScoped<IAdminUserHttpRepository, AdminUsersvc>();
            builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);
            



            // configure http client
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44303/api/") });

            var host = builder.Build();

            var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
            await authenticationService.Initialize();

            await host.RunAsync();




        }
    }
}
