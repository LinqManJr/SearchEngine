using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Engines;
using SearchEngine.Core.Services;
using SearchEngine.Domain.Context;
using SearchEngine.RazorPages.Services;

namespace SearchEngine.RazorPages
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

            services.AddOptions();

            var configSection = Configuration.GetSection("EnginesConfig");
            services.Configure<SearchEngineOptions>(configSection.GetSection("Bing"));
            services.Configure<GoogleSearchOptions>(configSection.GetSection("Google"));
            services.Configure<YandexSearchOptions>(configSection.GetSection("Yandex"));

            services.AddScoped<ISearchEngine, BingSearchEngine>();
            services.AddScoped<ISearchEngine, GoogleSearchEngine>();
            services.AddScoped<ISearchEngine, YandexSearchEngine>();
           
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IDatabaseService, SearchDbService>();
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
                endpoints.MapRazorPages();
            });
        }
    }
}
