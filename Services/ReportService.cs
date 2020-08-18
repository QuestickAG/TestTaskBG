using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskBarsGroup.Dto;
using TestTaskBarsGroup.Services;

namespace TestTaskBarsGroup
{
    public class ReportService
    {
        private readonly ApplicationContext _dbContext;

        public ReportService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ShowByOffice() // 1 report
        {
            var offices = _dbContext.Office
                .Select(offices => new OfficeDto
                {
                    Id = offices.Id,
                    OfficeName = offices.OfficeName,
                    OfficeCityName = offices.OfficeCityName
                })
                .OrderBy(s => s.OfficeName)
                .ToList();
            
            foreach (var office in offices)
            {
                Console.WriteLine($"id - {office.Id}) {office.OfficeName} в городе {office.OfficeCityName}");
            }
        }

        public void ShowByDepartment() // 2 report 
        {
            var departments = _dbContext.Departments
                .Select(departments => new DepartmentDto
                {
                    Id = departments.Id,
                    DepartmentName = departments.DepartmentName
                })
                .OrderBy(s=>s.DepartmentName)
                .ToList();

            foreach (var dapartment in departments)
            {
                Console.WriteLine($"id - {dapartment.Id}) {dapartment.DepartmentName}");
            }
        }

        public void ShowEmployeeCountByOffice() // 3 report
        {
            var employeesByOffice = _dbContext.Employees
                .Include(x => x.Office)
                .Select(x => new 
                {
                    OfficeId = x.Office.Id,
                    OfficeName = x.Office.OfficeName
                })
                .AsEnumerable()
                .GroupBy(x => x.OfficeId)
                .Select(group => new
                {
                    OfficeName = group.First().OfficeName,
                    Count = group.Count()
                })
                .ToList();

            foreach (var employeeByOffice in employeesByOffice)
            {
                Console.WriteLine($"{employeeByOffice.OfficeName} : {employeeByOffice.Count}");
                Console.WriteLine();
            }
        }

        public void ShowPaymentList(int officeId, int departmentId) // 4 report
        {

            var employeesByOfficeAndByDepartment = _dbContext.Employees
                .Where(x => x.Office.Id == officeId)
                .Where(x => x.Department.Id == departmentId)
                .Select(x => x.Id);

            var employeesSalary = _dbContext.Salary
                .Where(x => employeesByOfficeAndByDepartment.Contains(x.Employee.Id))
                .Select(x => new
                {
                    EmployeeName = x.Employee.Name,
                    EmployeeSurname = x.Employee.Surname,
                    Month = x.DateTime.ToString("Y"),
                    EmployeeSalary = x.SalaryForMonth 
                })
                .ToList();

            foreach (var employeeSalary in employeesSalary)
            {
                Console.WriteLine($"{employeeSalary.EmployeeSurname} {employeeSalary.EmployeeName}: {employeeSalary.Month} Зарплата: {employeeSalary.EmployeeSalary}");
                Console.WriteLine();
            }
        }
        /*
        public void ShowEmployeeCountByOfficeAndDepartment() // 5 report
        {
            var employeesByOffice = _dbContext.Employees
                .Include(x => x.Office)
                .Include(x=>x.Department)
                .Select(x => new
                {
                    OfficeName = x.Office.OfficeName,
                    DepartmentName = x.Department.DepartmentName
                })
                .AsEnumerable()
                .GroupBy(x => x.OfficeName)
                .Select(group => new
                {
                    OfficeName = group.First().OfficeName,
                    Count = group.Count()
                })
                .ToList();

            foreach (var employeeByOffice in employeesByOffice)
            {
                Console.WriteLine($"{employeeByOffice.OfficeName} : {employeeByOffice.Count}");
                Console.WriteLine();
            }
        }
        */

        public void ShowOfficeSalaryAverage() // 6 report
        {
            var OfficesSaalaryAverage = _dbContext.Salary
                .Include(x => x.Employee)
                .Where(x => x.DateTime.Month == DateTime.Now.Month)
                .Select(x => new
                {
                    OfficeName = x.Employee.Office.OfficeName,
                    EmployeeSalary = x.SalaryForMonth
                })
                .AsEnumerable()
                .GroupBy(x => x.OfficeName)
                .Select(group => new
                {
                    OfficeName = group.First().OfficeName,
                    SalaryAverage = group.Average(x =>x.EmployeeSalary)
                })
                .ToList();
            
            foreach (var OfficeSaalaryAverage in OfficesSaalaryAverage)
            {
                Console.WriteLine($"{OfficeSaalaryAverage.OfficeName} : {OfficeSaalaryAverage.SalaryAverage}");
                Console.WriteLine();
            }
        } 

        public void ShowEmployeeSalaryMoreN(decimal Number) // 7 report
        {
            var employeesSalary = _dbContext.Salary
                .Include(x =>x.Employee)
                .Where(x=>x.SalaryForMonth > Number)
                .Select(x => new
                {
                    EmployeeName = x.Employee.Name,
                    EmployeeSurname = x.Employee.Surname,
                    Month = x.DateTime.ToString("Y"),
                    Salary = x.SalaryForMonth
                })
                .AsEnumerable()
                .ToList();

            foreach (var employeeSalary in employeesSalary)
            {
                Console.WriteLine($"{employeeSalary.EmployeeSurname} {employeeSalary.EmployeeName}: " +
                    $"месяц {employeeSalary.Month} - зарплата: {employeeSalary.Salary}");
                Console.WriteLine();
            }
        }

        public void ShowEmployeesWorkedAllHour() // 8 report
        {
            var EmployeesFixedSalaryWorkedAllHour = _dbContext.Hours
                .Include(x => x.Employee)
                .Where(x => x.Employee.SalaryType == SalaryType.Fixed)
                .Where(x => x.Hour == 150)
                .Select(x => new
                {
                    EmployeeName = x.Employee.Name,
                    EmployeeSurname = x.Employee.Surname,
                    Month = x.DateTime.ToString("Y"),
                    Hours = x.Hour
                })
                .AsEnumerable()
                .ToList();

            foreach (var EmployeeFixedSalaryWorkedAllHour in EmployeesFixedSalaryWorkedAllHour)
            {
                Console.WriteLine($"{EmployeeFixedSalaryWorkedAllHour.EmployeeSurname} {EmployeeFixedSalaryWorkedAllHour.EmployeeName}: " +
                    $"месяц {EmployeeFixedSalaryWorkedAllHour.Month} - отработал часов: {EmployeeFixedSalaryWorkedAllHour.Hours}");
                Console.WriteLine();
            }
        }

        public void ShowEmployeesMaxSalary(int Count) // 9 report
        {
            var EmployeesMaxSalary = _dbContext.Salary
                .Include(x => x.Employee)
                .Where(x => x.DateTime.Month == DateTime.Now.Month)
                .Select(x => new
                {
                    EmployeeName = x.Employee.Name,
                    EmployeeSurname = x.Employee.Surname,
                    Month = x.DateTime.ToString("Y"),
                    Salary = x.SalaryForMonth
                })
                .OrderByDescending(x => x.Salary)
                .Take(Count)
                .ToList();

            foreach (var EmployeeMaxSalary in EmployeesMaxSalary)
            {
                Console.WriteLine($"{EmployeeMaxSalary.EmployeeSurname} {EmployeeMaxSalary.EmployeeName}: " +
                    $"месяц {EmployeeMaxSalary.Month} - зарплата: {EmployeeMaxSalary.Salary}");
                Console.WriteLine();
            }
        }
    }
}
