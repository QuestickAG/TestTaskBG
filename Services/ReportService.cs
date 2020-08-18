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

        public void ShowByOffice()
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

        public void ShowByDepartment()
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

        public void ShowEmployeeCountByOffice()
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

        public void ShowPaymentList(int officeId, int departmentId)
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
        public void ShowEmployeeCountByOfficeAndDepartment()
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

        public void ShowOfficeSalaryAverage()
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

        public void ShowEmployeeSalaryMoreN(decimal Number)
        {
            var employeeSalary = _dbContext.Salary
                .Include(x =>x.Employee)
                .Where(x=>x.SalaryForMonth > Number)
                .Select(x => new
                {
                    EmployeeName = x.Employee.Name,
                    EmployeeSurname = x.Employee.Surname,
                    Month = x.DateTime.ToString("Y"),
                    Salary = x.SalaryForMonth
                })
                .ToList();

        }

    }
}
