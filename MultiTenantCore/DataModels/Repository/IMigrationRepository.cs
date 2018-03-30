using MultiTenantCore.DataModels.Contexts;

namespace MultiTenantCore.DataModels.Repository
{
    public interface IMigrationRepository
    {
        void AddMigration();
    }
}