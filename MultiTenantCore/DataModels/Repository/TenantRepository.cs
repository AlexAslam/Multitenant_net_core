using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MultiTenantCore.DataModels.Contexts;
using MultiTenantCore.DataModels.Entities;
using MultiTenantCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantCore.DataModels.Repository
{
    public class TenantRepository : ITenantRepository
    {
        private readonly TenantContext _context;
        private readonly ILogger<TenantRepository> _logger;
        private readonly IConfiguration _configuration;
        
        public TenantRepository(ILogger<TenantRepository> logger, 
            TenantContext context,
            IConfiguration configuration
            )
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        
        public IEnumerable<Tenant> getAllTenants()
        {
            try
            {
                return _context.Tenants;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Tenant getTenantById(int id)
        {
            try
            {
                return _context.Tenants.Where(c => c.Id == id).Last();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public void saveAll()
        {
            _context.SaveChangesAsync();
        }

        public string onTenantEntry(Tenant connection_name)
        {
            var databaseName = _configuration["DatabaseData:DatabaseName"];
            var ServerName = _configuration["DatabaseData:Server"];
            var PortName = _configuration["DatabaseData:Port"];
            var UserIdName = _configuration["DatabaseData:UserId"];
            var PasswordName = _configuration["DatabaseData:password"];
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var connectionString = $"server=localhost;Port=5432;User Id=postgres;password=alex;DataBase=MultiTenantCore{connection_name.SubDomainName};Integrated Security=true;";
            dbContextOptionsBuilder.UseNpgsql(@connectionString);
            ApplicationContext context = new ApplicationContext(dbContextOptionsBuilder.Options);
            context.Database.EnsureCreatedAsync();
            return connectionString;
            //DbContextFactory.Create();
            //_dbContextOptions. .UseNpgsql(@"server=localhost;Port=5432;User Id=postgres;password=alex;DataBase=MultiTenantAlche;Integrated Security=true;");

            /*_dbContextOptionsBuilder.UseNpgsql($"server={ServerName};Port={PortName};User Id={UserIdName};password={PasswordName};DataBase={connection_name.SubDomainName};Integrated Security=true;");*/
        }

        public void addEntity(Tenant newTenant)
        {
            _context.Add(newTenant);
            saveAll();
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<TenantContext>();
            dbContextOptionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnectionString"));
            TenantContext tenantContext_temp = new TenantContext(dbContextOptionsBuilder.Options);
            Tenant recent_tenant = tenantContext_temp.Tenants.Last();
            recent_tenant.ConnectionStringName = onTenantEntry(newTenant);
            tenantContext_temp.SaveChanges();
        }
    }
}
