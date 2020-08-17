using System;
using System.Collections.Generic;
using System.Text;
using TestTaskBarsGroup.Model;
using TestTaskBarsGroup.Models;

namespace TestTaskBarsGroup
{
    
    public class Employee
    {
        public int Id { get; set; }

        public Office Office { get; set; }

        public Department Department { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public string PostName { get; set; }

        public SalaryType SalaryType { get; set; }

        public decimal Payment { get; set; }
    }
}
