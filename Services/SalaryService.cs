using System;
using System.Collections.Generic;
using System.Text;
using TestTaskBarsGroup.Dto;
using TestTaskBarsGroup.Models;

namespace TestTaskBarsGroup.Services
{
    public class SalaryService
    {
        private readonly ApplicationContext _dbContext;

        public SalaryService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddHours(HourPerMonthDto hourPerMonth)
        {
            var employeeHour = new HourPerMonth
            {
                Employee = _dbContext.Employees.Find(hourPerMonth.Employee),
                Hour = hourPerMonth.Hour,
                DateTime = hourPerMonth.DateTime
                };

            _dbContext.Add(employeeHour);
            _dbContext.SaveChanges();
        }

        public void AddSalaryForMonth(SalaryPerMonthDto salaryPerMonth)
        {
            var employeeSalary = new SalaryPerMonth
            {
                Employee = _dbContext.Employees.Find(salaryPerMonth.Employee),
                DateTime = salaryPerMonth.DateTime,
                SalaryForMonth = salaryPerMonth.SalaryForMonth
            };
        }

    }
}
