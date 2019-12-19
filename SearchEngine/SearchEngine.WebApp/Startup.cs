using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SearchEngine.Domain.Context;
using SearchEngine.WebApp.Services;

namespace SearchEngine.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("SEConnection");
            services.AddDbContext<SearchContext>(options => options.UseSqlServer(connectionString));            

            services.AddScoped<SearchServiceFactory>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IDatabaseService, SearchDbService>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {                             
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Search}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
