using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantCore.DataModels.Contexts;

namespace MultiTenantCore.DataModels
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project

    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly TenantContext _tenantContext;
        public Middleware(RequestDelegate next)
        {
            DbContextOptions<TenantContext> options = new DbContextOptions<TenantContext>();
            //options.UseNpgsql(@"server=localhost;Port=5432;User Id=postgres;password=alex;DataBase=MultiTenantCore;Integrated Security=true;");
            _tenantContext = new TenantContext(options);
            _next = next;
            System.Console.WriteLine($"Middleware class ===============================>: MiddleWare Method");
        }

        public Task Invoke(HttpContext httpContext)
        {
            var subdomain = httpContext.Request.Host.Host.Split('.')[0];
            System.Console.WriteLine($"Middleware class ===============================>: Invoke Method ==>{subdomain}");
            if (_tenantContext.Tenants.Where(c => c.SubDomainName == subdomain).Count() > 0)
            {
                
                //DbContextOptionsBuilder<ApplicationContext> appoptions = new DbContextOptionsBuilder<ApplicationContext>();
                
                var connection_string =_tenantContext.Tenants.Where(c => c.SubDomainName == subdomain && (c.ConnectionStringName != null && c.ConnectionStringName != "" )).Last().ConnectionStringName;
                //appoptions.UseNpgsql(connection_string);
                Environment.SetEnvironmentVariable("DATABASE_VARIABLE_FOR_APPLICATION_CONTEXT", $"{connection_string}");
                System.Console.WriteLine(Environment.GetEnvironmentVariable("DATABASE_VARIABLE_FOR_APPLICATION_CONTEXT"));
                //ApplicationContext _applicationContext = new ApplicationContext(appoptions.Options);
                //System.Console.WriteLine($"Middleware class ===============================>: Invoke Method ==>{_applicationContext.Employees.Count()}");
                System.Console.WriteLine($"Middleware class ===============================>: Environment Variable ==>{Environment.GetEnvironmentVariable("DATABASE_VARIABLE_FOR_APPLICATION_CONTEXT")}");
            }
            else
            {
                Environment.SetEnvironmentVariable("DATABASE_VARIABLE_FOR_APPLICATION_CONTEXT", $"{null}");
                System.Console.WriteLine($"Middleware class ===============================>: Invoke Method ==> i think it got some problem");
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            System.Console.WriteLine($"Middleware class ===============================>: IApplicationBuilder");
            
            return builder.UseMiddleware<Middleware>();
        }

    }
}
