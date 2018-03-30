using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiTenantCore.DataModels.Entities;

namespace MultiTenantCore.DataModels.Contexts
{
    public class TenantContext : DbContext
    {
        public TenantContext(DbContextOptions<TenantContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(@"server=localhost;Port=5432;User Id=postgres;password=alex;DataBase=MultiTenantCore;Integrated Security=true;");
        }
        public DbSet<Tenant> Tenants { get; set; }
        
    }
}
