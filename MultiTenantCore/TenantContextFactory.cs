using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MultiTenantCore.DataModels.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantCore
{
    public class TenantContextFactory : IDesignTimeDbContextFactory<TenantContext>
    {
        public TenantContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TenantContext>();
            //IConfiguration configuration = new ConfigurationBuilder()
            //  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //  .AddJsonFile("config.json", false, true)
            //  .Build();
            System.Console.WriteLine($"app===============================>: {AppDomain.CurrentDomain}");
            builder.UseNpgsql(@"server=localhost;Port=5432;User Id=postgres;password=alex;DataBase=MultiTenantCore;Integrated Security=true;");
            TenantContext context = new TenantContext(builder.Options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
