using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantCore.DataModels;
using MultiTenantCore.DataModels.Contexts;
using MultiTenantCore.DataModels.Repository;
using MultiTenantCore.Helpers;

namespace MultiTenantCore
{
    public class Startup
    {
        private readonly IConfiguration _config;
        
        public Startup(IConfiguration config)
        {
            _config = config;
            System.Console.WriteLine($"app===============================>: startup config");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            System.Console.WriteLine($"app===============================>: startup services");
            services.AddDbContext<TenantContext>();
            services.AddDbContext<ApplicationContext>();
            services.AddScoped<HeaderInService>();
            services.AddAutoMapper();
            services.AddTransient<AppSeeder>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IMigrationRepository, MigrationRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            System.Console.WriteLine($"app===============================>: {env.EnvironmentName}");
            app.UseMvc();
           
                using (var scope = app.ApplicationServices.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<AppSeeder>();
                seeder.AddMigrations();
            }
        }
        private static string GetSubDomain(HttpContext httpContext)
        {
            var subDomain = string.Empty;

            var host = httpContext.Request.Host.Host;

            if (!string.IsNullOrWhiteSpace(host))
            {
                subDomain = host.Split('.')[0];
            }

            return subDomain.Trim().ToLower();
        }
    }
}
