using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaskBarsGroup.Models
{
    public class SalaryPerMonth
    {
        public int Id { get; set; }

        public Employee Employee { get; set; }

        public DateTime DateTime { get; set; }

        public decimal SalaryForMonth { get; set; }
    }
}
