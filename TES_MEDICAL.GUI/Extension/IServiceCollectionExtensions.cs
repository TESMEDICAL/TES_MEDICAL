


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.GUI.Services;

namespace TES_MEDICAL.GUI.Extension
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with Scoped lifetime
            services.AddDbContext<DataContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DataContextConnection"));
                    options.UseLazyLoadingProxies();
                }
            );



            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient<IBenh, Benhsvc>()
                .AddScoped<IDichVu, DichVusvc>()
                .AddScoped<IThuoc, Thuocsvc>()
                .AddScoped<IChuyenKhoa, ChuyenKhoasvc>();



        }

      
    }
}
