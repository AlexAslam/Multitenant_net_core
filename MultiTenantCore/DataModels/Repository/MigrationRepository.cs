using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MultiTenantCore.DataModels.Contexts;

namespace MultiTenantCore.DataModels.Repository
{
    public class MigrationRepository : IMigrationRepository
    {
        private readonly ApplicationContext _applicationContext;
        public MigrationRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void AddMigration()
        {
            _applicationContext.Database.Migrate();
        }
    }
}
