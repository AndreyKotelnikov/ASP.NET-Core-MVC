using ASP_NET_Core_MVC.Infrastructure.Conventions;
using ASP_NET_Core_MVC.Infrastructure.Interfaces;
using ASP_NET_Core_MVC.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            services.AddScoped<IProductData, InMemoryProductData>();
            
            services.AddMvc(op =>
            {
                op.EnableEndpointRouting = false;
                //op.Conventions.Add(new CustomControllerConvention());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            //examples of middleware
            //app.UseWelcomePage("/Welcome"); 
            ////app.Run(async context => await context.Response.WriteAsync("Hello world!")); //Unconditional execution with self-locking
            //app.Map("/Hello",
            //    application => application.Run(async context => await context.Response.WriteAsync("Hello world!")));
            //app.UseAuthentication();
            ////app.UseSession();
            //app.UseResponseCaching();
            ////app.UseResponseCompression();

            app.UseStaticFiles(new StaticFileOptions {ServeUnknownFileTypes = true});
            app.UseDefaultFiles();
            app.UseCookiePolicy();

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync(Configuration["CustomData"]);
            //    });
            //});

            //app.UseMvcWithDefaultRoute();

            app.UseMvc(routes => routes.MapRoute(
                "default",
                "{Controller=Home}/{action=Index}/{id?}"));
        }
    }
}
