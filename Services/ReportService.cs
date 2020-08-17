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
        /*
        public void ShowEmployeeCountByOffice()
        {
            var offices = _dbContext.Office
                .Select(offices => new OfficeDto
                {
                    Id = offices.Id,
                    OfficeName = offices.OfficeName,
                    OfficeCityName = offices.OfficeCityName
                })
                .ToList();
            foreach (var office in offices)
            {
                Console.WriteLine($"{office.OfficeName} кол-во сотрудников: {GetEmployeeList(office.Id)}");
            }
        }
        public int GetEmployeeList(int id)
        {
            var employees = _dbContext.Employees
                .Select(employees => new EmployeeDto
                {
                    Id = employees.Id
                })
                .Count(s => s.OfficeId == id);
                
            return employees;
        }
        */
    }
}
