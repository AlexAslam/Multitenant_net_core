using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiTenantCore.DataModels.Contexts;
using MultiTenantCore.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantCore.DataModels
{
    public class AppSeeder
    {
        private readonly TenantContext _tenantContext;
        private readonly ApplicationContext _applicationContext;
        private readonly IConfiguration _configuration;
        public AppSeeder(TenantContext tenantContext,ApplicationContext applicationContext,IConfiguration configuration)
        {
            _tenantContext = tenantContext;
            _applicationContext = applicationContext;
            _configuration = configuration;
        }
        public void AddMigrations()
        {
            var tenants = _tenantContext.Tenants.ToList();
            foreach (Tenant newtenant in tenants)
            {
                try
                {
                    var dbContextOptionsBuilder_ = new DbContextOptionsBuilder<ApplicationContext>();
                    dbContextOptionsBuilder_.UseNpgsql(newtenant.ConnectionStringName);
                    if (_applicationContext.Database.EnsureCreated())
                    {
                        _applicationContext.Database.Migrate();
                    }
                }
                catch
                {
                }
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<TenantContext>();
                dbContextOptionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnectionString"));
            }
        }
    }
}
