using ASP_NET_Core_MVC.Data;
using ASP_NET_Core_MVC.Infrastructure.Interfaces;
using ASP_NET_Core_MVC.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using PageActionEndpointConventionBuilder = Microsoft.AspNetCore.Builder.PageActionEndpointConventionBuilder;

namespace ASP_NET_Core_MVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<WebStoreDbContextInitializer>();

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            services.AddScoped<IProductData, SqlUnitOfWork>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<WebStoreDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(GetIdentityOptions);
            services.ConfigureApplicationCookie(GetCookieOptions);

            services.AddMvc(op =>
            {
                op.EnableEndpointRouting = false;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDbContextInitializer dbContextInit)
        {
            dbContextInit.InitializeAsync().Wait();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles(new StaticFileOptions {ServeUnknownFileTypes = true});
            app.UseDefaultFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes => routes.MapRoute(
                "default",
                "{Controller=Home}/{action=Index}/{id?}"));
        }

        private void GetIdentityOptions(IdentityOptions options)
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredUniqueChars = 3;

            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.MaxFailedAccessAttempts = 10;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);

            options.User.RequireUniqueEmail = false;
        }

        private void GetCookieOptions(CookieAuthenticationOptions options)
        {
            options.Cookie.Name = "WebStore-Identity";
            options.Cookie.HttpOnly = true;

            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.AccessDeniedPath = "/Account/AccessDenied";

            options.ExpireTimeSpan = TimeSpan.FromDays(150);
            options.SlidingExpiration = true;
        }
    }
}
