using ASP_NET_Core_MVC.Data;
using ASP_NET_Core_MVC.Infrastructure.Interfaces;
using ASP_NET_Core_MVC.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.DAL.Context;

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

            app.UseMvc(routes => routes.MapRoute(
                "default",
                "{Controller=Home}/{action=Index}/{id?}"));
        }
    }
}
