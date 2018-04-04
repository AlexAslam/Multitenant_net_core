using MultiTenantCore.DataModels.Entities;
using System.Collections.Generic;

namespace MultiTenantCore.DataModels.Repository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> getAllEmployees();
        Employee getEmployeeById(int id);
        void saveAll();
        void addEntity(Employee newEmployee);
    }
}