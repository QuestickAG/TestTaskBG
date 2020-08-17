using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskBarsGroup.Dto;
using TestTaskBarsGroup.Model;

namespace TestTaskBarsGroup
{
    public class DepartmentService
    {
        private readonly ApplicationContext _dbContext;

        public DepartmentService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddDepartment(string name)
        {
            var department = new Department()
            {
                DepartmentName = name
            };

            _dbContext.Add(department);
            _dbContext.SaveChanges();
        }

        public bool RemoveDepartment(int id)
        {
            if (_dbContext.Departments.Find(id) != null)
            {
                _dbContext.Departments.Remove(_dbContext.Departments.Find(id));
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<DepartmentDto> GetDepartments()
        {
            var departments = _dbContext.Departments
                .Select(departments => new DepartmentDto
                {
                    Id =departments.Id,
                    DepartmentName = departments.DepartmentName
                })
                .ToList();
            return departments;
        }
        public void ShowDepartments(List<DepartmentDto> departments)
        {
            Console.WriteLine(" id  Название подразделения");
            foreach (var dapartment in departments)
            {
                Console.WriteLine($" {dapartment.Id}) {dapartment.DepartmentName}");
            }
        }
    }
}
