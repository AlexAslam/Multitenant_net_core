using MultiTenantCore.DataModels.Entities;
using System.Collections.Generic;

namespace MultiTenantCore.DataModels.Repository
{
    public interface ITenantRepository
    {
        IEnumerable<Tenant> getAllTenants();
        Tenant getTenantById(int id);
        void saveAll();
        bool addEntity(Tenant newTenant);
    }
}