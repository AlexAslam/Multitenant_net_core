using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MultiTenantCore.DataModels.Contexts;
using MultiTenantCore.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiTenantCore.DataModels.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<EmployeeRepository> _logger;
        private readonly IConfiguration _configuration;

        public EmployeeRepository(ILogger<EmployeeRepository> logger,
            IConfiguration configuration,
            ApplicationContext context
            )
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public IEnumerable<Employee> getAllEmployees()
        {
            try
            {
                return _context.Employees;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Employee getEmployeeById(int id)
        {
            try
            {
                return _context.Employees.Where(c => c.Id == id).Last();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void saveAll()
        {
             _context.SaveChanges();
        }

        public void addEntity(Employee newEmployee)
        {
            _context.Add(newEmployee);
            saveAll();
        }
    }
}
