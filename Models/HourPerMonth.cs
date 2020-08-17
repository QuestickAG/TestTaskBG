using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaskBarsGroup.Models
{
    public class HourPerMonth
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }

        public int Hour { get; set; }

        public DateTime DateTime { get; set; }
    }
}
