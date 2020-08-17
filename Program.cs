using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using TestTaskBarsGroup.Services;

namespace TestTaskBarsGroup
{
    class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<WorkService>()
                .AddSingleton<DepartmentService>()
                .AddSingleton<EmployeeService>()
                .AddSingleton<OfficeService>()
                .AddSingleton<SalaryService>()
                .AddSingleton<ReportService>()
                .AddDbContext<ApplicationContext>()
                .BuildServiceProvider();
            
            var workService = serviceProvider
                .GetService<WorkService>();

            workService.Work();
        }
    }
}
