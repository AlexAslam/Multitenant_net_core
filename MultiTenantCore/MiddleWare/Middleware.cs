using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
            _tenantContext = new TenantContext(options);
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var subdomain = httpContext.Request.Host.Host.Split('.')[0];
            if ((_tenantContext.Tenants.Where(c => c.SubDomainName == subdomain).Count() > 0)&& subdomain != "localhost" )
            {
                if (_tenantContext.Tenants.Where(c => c.SubDomainName == subdomain && (c.ConnectionStringName != null && c.ConnectionStringName != "")).Count() > 0)
                {
                    var connection_string = _tenantContext.Tenants.Where(c => c.SubDomainName == subdomain && (c.ConnectionStringName != null && c.ConnectionStringName != "")).Last().ConnectionStringName;
                    Environment.SetEnvironmentVariable("DATABASE_VARIABLE_FOR_APPLICATION_CONTEXT", $"{connection_string}");
                }
                else
                {
                    return httpContext.Response.WriteAsync("No DataBase Found!");
                }
            }
            else if ((_tenantContext.Tenants.Where(c => c.SubDomainName == subdomain).Count() == 0) && subdomain != "localhost")
            {
                Environment.SetEnvironmentVariable("DATABASE_VARIABLE_FOR_APPLICATION_CONTEXT", $"{null}");
                return httpContext.Response.WriteAsync("No DataBase Found!");
            }
            else
            {
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }

    }
}
