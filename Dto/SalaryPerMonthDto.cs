using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaskBarsGroup.Dto
{
    public class SalaryPerMonthDto
    {
        public int Id { get; set; }

        public int Employee { get; set; }

        public DateTime DateTime { get; set; }

        public decimal SalaryForMonth { get; set; }
    }
}
