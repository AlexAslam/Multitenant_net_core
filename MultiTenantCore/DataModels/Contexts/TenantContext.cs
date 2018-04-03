using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiTenantCore.DataModels.Entities;
using MultiTenantCore.DataModels.Repository;
namespace MultiTenantCore.DataModels.Contexts
{
    public class TenantContext : DbContext
    {
        public TenantContext(DbContextOptions<TenantContext> options) : base(options)
        {
            System.Console.WriteLine($"DbContextOptions= TenantContext ApplicationContext ==============================>: ");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            System.Console.WriteLine($"DbContextOptions= TenantContext modelBuilder =================================>:");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            System.Console.WriteLine($"DbContextOptions= TenantContext optionsBuilder =================================>:");
            optionsBuilder.UseNpgsql(@"server=localhost;Port=5432;User Id=postgres;password=alex;DataBase=MultiTenantCore;Integrated Security=true;");
        }
        public DbSet<Tenant> Tenants { get; set; }
        
    }
}
