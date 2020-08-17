using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaskBarsGroup
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public string PostName { get; set; }

        public SalaryType SalaryType { get; set; }

        public decimal Payment { get; set; }
    }
}
