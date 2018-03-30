using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiTenantCore.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantCore.DataModels.Contexts
{
    public class ApplicationContext : DbContext
    {
        private readonly TenantContext _tenantContext;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public ApplicationContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            foreach (Tenant newTenant in TenantContext.Tenants.ToList()) {
                optionsBuilder.UseNpgsql(@newTenant.ConnectionStringName);
            }
        }


        public DbSet<Employee> Employees { get; set; }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        public TenantContext TenantContext => _tenantContext;
    }
}
