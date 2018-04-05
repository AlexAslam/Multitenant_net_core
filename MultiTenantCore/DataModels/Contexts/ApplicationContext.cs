using Microsoft.EntityFrameworkCore;
using MultiTenantCore.DataModels.Entities;
using System;

namespace MultiTenantCore.DataModels.Contexts
{
    public class ApplicationContext : DbContext
    { 
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (Environment.GetEnvironmentVariable("DATABASE_VARIABLE_FOR_APPLICATION_CONTEXT") !=null && Environment.GetEnvironmentVariable("DATABASE_VARIABLE_FOR_APPLICATION_CONTEXT") != "")
            {
                optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_VARIABLE_FOR_APPLICATION_CONTEXT"));
            }
            
        }


        public DbSet<Employee> Employees { get; set; }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}
