using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddSalaryForMonth()
        {
            var employees = GetEmployees();
            var emplyeesHour = GetEmployeesHour(employees.Select(x=>x.Id).ToList());
            var month = DateTime.Now;
            foreach (var employee in employees)
            {
                var hour = emplyeesHour
                    .Where(x =>x.EmployeeId == employee.Id)
                    .Select(x => x.EmployeeHour)
                    .LastOrDefault();
                if (hour != default(int))
                {
                    var type = employee.SalaryType;
                    decimal salaryEmployee;
                    if (type == SalaryType.Fixed)
                    {
                        salaryEmployee = employee.Payment / 150 * hour;
                    }
                    else
                    {
                        salaryEmployee = employee.Payment * hour;
                    }

                    var salaryPerMonth = new SalaryPerMonth()
                    {
                        Employee = employee,
                        DateTime = month,
                        SalaryForMonth = salaryEmployee
                    };
                    _dbContext.Add(salaryPerMonth);
                }
            }
            _dbContext.SaveChanges();
        }

        public List<EmployeeHourDto> GetEmployeesHour(List<int> employeeIds)
        {
            var employeeHour = _dbContext.Hours
                .Where(x => employeeIds.Contains(x.Employee.Id))
                .Where(x => x.DateTime.Month == DateTime.Now.Month)
                .Select(x => new EmployeeHourDto
                {
                    EmployeeId = x.Employee.Id,
                    EmployeeHour = x.Hour
                })
                .ToList();
            return employeeHour;
        }

        public List<Employee> GetEmployees()
        {
            var employees = _dbContext.Employees
               .ToList();
            return employees;
        }
    }
}
