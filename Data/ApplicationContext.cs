using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TestTaskBarsGroup.Model;
using TestTaskBarsGroup.Models;

namespace TestTaskBarsGroup
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Office> Office { get; set; }

        public DbSet<HourPerMonth> Hours { get; set; }

        public DbSet<SalaryPerMonth> Salary { get; set; }

        /*public ApplicationContext()
        {
            Database.EnsureCreated();
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestTaskBarsGroup;Trusted_Connection=True;");
        }

    }
}
