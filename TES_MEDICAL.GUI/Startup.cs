using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.ResponseCompression;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TES_MEDICAL.GUI.Extension;
using TES_MEDICAL.GUI.Helpers;
using TES_MEDICAL.GUI.Infrastructure;
using TES_MEDICAL.GUI.Interfaces;
using TES_MEDICAL.GUI.Models;
using TES_MEDICAL.GUI.Services;

using Hangfire;

using Microsoft.AspNetCore.Identity;
using System.Net;


namespace TES_MEDICAL.GUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Token tồn tại trong 2 tiếng
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromHours(2));

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)

            services.AddSession(option => { option.IdleTimeout = TimeSpan.FromMinutes(120); });
            //services.ConfigureApplicationCookie(options =>
            //{

            //    options.Cookie.Name = ".AspNetCore.Identity.Application";
            //    //options.ExpireTimeSpan = TimeSpan.FromHours(1);
            //    options.SlidingExpiration = true;
            //});

            //        services.AddAuthentication()
            //.AddGoogle(googleOptions =>
            //{
            //    // Đọc thông tin Authentication:Google từ appsettings.json
            //    IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");

            //    // Thiết lập ClientID và ClientSecret để truy cập API google
            //    googleOptions.ClientId = googleAuthNSection["ClientId"];
            //    googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];

            //});

            var jwtSettings = Configuration.GetSection("JWTSettings");
            services.AddAuthentication()
    .AddCookie(options =>
    {


        options.SlidingExpiration = true;

    }
    )

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });


            services.AddControllersWithViews().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //services.AddDefaultIdentity<NhanVienYte>(options => options.SignIn.RequireConfirmedAccount = false).AddErrorDescriber<CustomErrorDescriber>()
            //       .AddEntityFrameworkStores<DataContext>();
            services.AddDefaultIdentity<NhanVienYte>(options => {
                options.SignIn.RequireConfirmedAccount = false;

            }).AddRoles<IdentityRole>().AddErrorDescriber<CustomErrorDescriber>()
                   .AddEntityFrameworkStores<DataContext>();
            services
              .AddDatabase(Configuration)

              .AddRepositories();

            services.AddSignalR();



            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));




            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                //.UseMemoryStorage() 
                .UseSqlServerStorage(Configuration.GetConnectionString("DataContextConnection"))
            );
            services.AddHangfireServer();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest)
   .AddRazorPagesOptions(options =>
   {
       options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
       options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
   }).AddRazorRuntimeCompilation();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = ".AspNetCore.Identity.Application";
                options.ExpireTimeSpan = TimeSpan.FromHours(2);
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });


        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("MyPolicy");

            app.UseAuthentication();

            app.UseAuthorization();
            //app.UseEndpoints(endpoints =>
            //{

            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=TiepNhan}/{action=ThemPhieuKham}");
                endpoints.MapHub<SignalServer>("/signalServer");
                endpoints.MapHub<RealtimeHub>("/PhieuKham");
                endpoints.MapRazorPages();
            });
            app.UseHangfireDashboard();
            recurringJobManager.AddOrUpdate(
                "Run every minute",
                () => serviceProvider.GetService<IAutoBackground>().AutoDelete(),
                "00 19 * * *"
                );
        }
    }
}
