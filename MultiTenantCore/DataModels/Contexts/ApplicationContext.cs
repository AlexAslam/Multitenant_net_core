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
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
            System.Console.WriteLine($"DbContextOptions= ApplicationContext ApplicationContext ==============================>: ");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            System.Console.WriteLine($"DbContextOptions= ModelBuilder ApplicationContext ==============================>: ");
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (Environment.GetEnvironmentVariable("database_variable_for_application_context")!=null && Environment.GetEnvironmentVariable("database_variable_for_application_context") != "")
            {
                System.Console.WriteLine($"DbContextOptions= DbContextOptionsBuilder ApplicationContext ==============================>:{Environment.GetEnvironmentVariable("database_variable_for_application_context")} ");
                optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("database_variable_for_application_context"));
            }
            
        }


        public DbSet<Employee> Employees { get; set; }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}
