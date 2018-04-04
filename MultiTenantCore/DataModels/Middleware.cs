using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MultiTenantCore.DataModels
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Middleware
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
            System.Console.WriteLine($"Middleware class ===============================>: MiddleWare Method");
        }

        public Task Invoke(HttpContext httpContext)
        {
            System.Console.WriteLine($"Middleware class ===============================>: Invoke Method");
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
