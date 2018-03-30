using Microsoft.EntityFrameworkCore;
using MultiTenantCore.DataModels.Entities;

namespace MultiTenantCore.DataModels.Contexts
{
    public class TenantContext : DbContext
    {
        public TenantContext(DbContextOptions<TenantContext> options) : base(options)
        {
            
        }
        public DbSet<Tenant> Tenants { get; set; }
    }
}
