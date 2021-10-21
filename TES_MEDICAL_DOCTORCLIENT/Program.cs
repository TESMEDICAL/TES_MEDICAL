using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TES_MEDICAL_DOCTORCLIENT
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

                          .AddScoped<IModal, ModalServices>()
                          .AddScoped<ILocalStorageService, LocalStorageService>()
                          .AddScoped<IHomeHttpRepository, Homesvc>()
                          .AddScoped<IKhachHangHttpRepository, KhachHangsvc>();


            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://duydtharry-001-site1.htempurl.com/") });
            var host = builder.Build();

            var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
            await authenticationService.Initialize();

            await host.RunAsync();

            await builder.Build().RunAsync();
        }
    }
}
