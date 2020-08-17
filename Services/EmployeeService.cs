using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTaskBarsGroup
{
    public class EmployeeService 
    {
        private readonly ApplicationContext _dbContext;

        public EmployeeService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddEmployee(EmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Surname = employeeDto.Surname,
                MiddleName = employeeDto.MiddleName,
                PostName = employeeDto.PostName,
                SalaryType = employeeDto.SalaryType,
                Office = _dbContext.Office.Find(employeeDto.OfficeId),
                Department = _dbContext.Departments.Find(employeeDto.DepartmentId)
            };

            _dbContext.Add(employee);
            _dbContext.SaveChanges();
        }

        public bool RemoveEmployee(int id)
        {
            var employee = _dbContext.Employees.Find(id);

            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public void ChangeSalaryType(int id, SalaryType type, decimal payment)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee.SalaryType == SalaryType.Fixed){
                employee.SalaryType = SalaryType.Hourly;
                employee.Payment = payment;
            } else if(employee.SalaryType == SalaryType.Hourly)
            {
                employee.SalaryType = SalaryType.Fixed;
                employee.Payment = payment;
            }
            _dbContext.SaveChanges();
        }
        public List<EmployeeDto> GetEmployee()
        {
            var employees = _dbContext.Employees
               .Select(employees => new EmployeeDto
               {
                   Id = employees.Id,
                   Surname = employees.Surname,
                   Name = employees.Name,
                   MiddleName = employees.MiddleName,
                   PostName = employees.PostName,
                   SalaryType = employees.SalaryType,
                   Payment = employees.Payment
               })
               .ToList();
            return employees;
        }
        public void ShowEmployees(List<EmployeeDto> employees)
        {
            Console.WriteLine(" id  Фамилия имя");
            foreach (var employee in employees)
            {
                Console.WriteLine($" {employee.Id}) {employee.Surname} {employee.Name} {employee.MiddleName} " +
                    $"{employee.PostName} {employee.SalaryType} {employee.Payment}");
            }
        }
    }
}
