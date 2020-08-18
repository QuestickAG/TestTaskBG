using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskBarsGroup.Dto;

namespace TestTaskBarsGroup.Services
{
    public class WorkService
    {
        private readonly DepartmentService _departmentService;
        private readonly OfficeService _officeService;
        private readonly EmployeeService _employeeService;
        private readonly ReportService _reportService;
        private readonly SalaryService _salaryService;

        public WorkService(DepartmentService departmentService,
            EmployeeService employeeService,
            ReportService reportService,
            SalaryService salaryService,
            OfficeService officeService)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
            _officeService = officeService;
            _reportService = reportService;
            _salaryService = salaryService;
        }

        public void Work()
        {
            string point;
           
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выбирите действие: " +
                    " \n1) Филиалы" +
                    " \n2) Подразделения" +
                    " \n3) Сотрудники" +
                    " \n4) Отчеты");
                point = Console.ReadLine();
                switch (point)
                {
                    case "1":
                        OfficeServices();
                        break;
                    case "2":
                        DepartmentServices();
                        break;
                    case "3":
                        EmployeeServices();
                        break;
                    case "4":
                        ReportServices();
                        break;
                    default:
                        Console.WriteLine("Такого действия нет");
                        break;
                }
            }
        }

        public void ReportServices()
        {
            var flag = true;
            while (flag)
            {
                Console.Clear();
                Console.WriteLine("Выбирите отчет: " +
                        "\n1) Список филиалов в алфавитном порядке " +
                        "\n2) Список подразделений в алфавитном порядке " +
                        "\n3) Список филиалов в алфавитном порядке с указанием кол-ва работающих сотрудников в филиале " +
                        "\n4) Расчетный лист за текущий месяц в виде для филиала и подразделения в виде списка сотрудников в алфавитном порядке с указанием суммы зарплаты" +
                        "\n5) Список филиалов в алфавитном порядке с указанием кол-ва работающих сотрудников в филиале с группировкой по подразделениям" +
                        "\n6) Список филиалов с указанием средней запрплаты в филиале" +
                        "\n7) Список сотрудников, размер зарплаты в текущем месяце которых > X " +
                        "\n8) Список сотрудников с фиксированной месячной оплатой, которые в текущем  месяце отработали все требуемые часы согласно трудовому календарю" +
                        "\n9) Список N сотрудников с наибольшим размером зарплаты в текущем месяце" +
                        "\n10) Назад");
                string point = Console.ReadLine();
                switch (point)
                {
                    case "1":
                        Console.Clear();
                        _reportService.ShowByOffice();
                        Console.WriteLine("Нажмите Enter чтобы вернутся");
                        Console.ReadLine();
                        break;

                    case "2":
                        Console.Clear();
                        _reportService.ShowByDepartment();
                        Console.WriteLine("Нажмите Enter чтобы вернутся");
                        Console.ReadLine();
                        break;

                    case "3":
                        Console.Clear();
                        _reportService.ShowEmployeeCountByOffice();
                        Console.WriteLine("Нажмите Enter чтобы вернутся");
                        Console.ReadLine();
                        break;

                    case "4":
                        var departmenstList = _departmentService.GetDepartments();
                        _departmentService.ShowDepartments(departmenstList);
                        Console.WriteLine("Введите id:");
                        int departmentId;
                        while (!int.TryParse(Console.ReadLine(), out departmentId) || !(departmenstList.Any(u => u.Id == departmentId)))
                        {
                            Console.WriteLine("введен не верный формат id");
                        }
                        
                        var officeList = _officeService.GetOffices();
                        _officeService.ShowOffices(officeList);
                        Console.WriteLine("Введите id:");
                        int officeId;
                        while (!int.TryParse(Console.ReadLine(), out officeId) || !(officeList.Any(u => u.Id == officeId)))
                        {
                            Console.WriteLine("введен не верный формат id");
                        }
                        _reportService.ShowPaymentList(officeId, departmentId);
                        Console.WriteLine("Нажмите Enter чтобы вернутся");
                        Console.ReadLine();
                        break;

                    case "5":
                        _reportService.ShowEmployeeCountByOfficeAndDepartment();
                        Console.WriteLine("Нажмите Enter чтобы вернутся");
                        Console.ReadLine();
                        break;

                    case "6":
                        _reportService.ShowOfficeSalaryAverage();
                        Console.WriteLine("Нажмите Enter чтобы вернутся");
                        Console.ReadLine();
                        break;

                    case "7":
                        Console.WriteLine("Введите суммы:");
                        int number;
                        while (!int.TryParse(Console.ReadLine(), out number) || !(number > 0))
                        {
                            Console.WriteLine("введен не верный формат суммы");
                        }
                        _reportService.ShowEmployeeSalaryMoreNumber(number);
                        Console.WriteLine("Нажмите Enter чтобы вернутся");
                        Console.ReadLine();
                        break;

                    case "8":
                        _reportService.ShowEmployeesWorkedAllHour();
                        Console.WriteLine("Нажмите Enter чтобы вернутся");
                        Console.ReadLine();
                        break;

                    case "9":
                        Console.WriteLine("Введите количество сотрудников:");
                        int count;
                        while (!int.TryParse(Console.ReadLine(), out count) || !(count > 0))
                        {
                            Console.WriteLine("введен не верный формат количества");
                        }
                        _reportService.ShowEmployeesMaxSalary(count);
                        Console.WriteLine("Нажмите Enter чтобы вернутся");
                        Console.ReadLine();
                        break;

                    case "10":
                        flag = false;
                        break;

                    default:
                        Console.WriteLine("Такого действия нет");
                        break;
                }
            }
        }

        public void DepartmentServices()
        {
            var flag = true;
            while (flag ) 
            {
                Console.Clear();
                Console.WriteLine("Выбирите действие: " +
                        "\n1) Добавить подразделение " +
                        "\n2) Удалить подразделение " +
                        "\n3) Показать подразделения" +
                        "\n4) Назад");
                string point = Console.ReadLine();
                var listDepartament = _departmentService.GetDepartments();
                switch (point)
                {
                    case "1":
                        Console.WriteLine("Введите название:");

                        string name = Console.ReadLine();

                        _departmentService.AddDepartment(name);

                        Console.WriteLine("Добавление успешно!");
                        flag = false;
                        break;
                    case "2":
                        
                        _departmentService.ShowDepartments(listDepartament);
                        Console.WriteLine("Введите id:");
                        int id;
                        while (!int.TryParse(Console.ReadLine(), out id) || !(listDepartament.Any(u => u.Id == id)))
                        {
                            Console.WriteLine("введен не верный формат id");
                        }
                        if (_departmentService.RemoveDepartment(id))
                        {
                            Console.WriteLine("Удаление успешно! Нажмите Enter!");
                            Console.ReadLine();
                            flag = false;
                        } 
                        else
                        {
                            Console.WriteLine("введен не верный id");
                        }
                        break;
                    case "3":
                        Console.Clear();
                        _departmentService.ShowDepartments(listDepartament);
                        Console.WriteLine("Нажмите Enter чтобы вернутся");
                        Console.ReadLine();
                        break;
                    case "4":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Такого действия нет");
                        break;
                }
            }
        }

        public void OfficeServices()
        {
            var flag = true;
            while (flag)
            {
                Console.Clear();
                Console.WriteLine("Выбирите действие: " +
                        "\n1) Добавить филиал " +
                        "\n2) Удалить филиал " +
                        "\n3) Показать филиалы " +
                        "\n4) Назад");
                string point = Console.ReadLine();
                var listOffices = _officeService.GetOffices();
                switch (point)
                {
                    case "1":
                        Console.WriteLine("Введите название филиала:");
                        var officeName = Console.ReadLine();
                        Console.WriteLine("Введите название города:");
                        var officeCityName = Console.ReadLine();
                        _officeService.AddOffice(officeName, officeCityName);
                        Console.WriteLine("Добавление успешно");
                        flag = false;
                        break;
                    case "2":
                        
                        _officeService.ShowOffices(listOffices);
                        int id;
                        while (!int.TryParse(Console.ReadLine(), out id) || !(listOffices.Any(u => u.Id == id)))
                        {
                            Console.WriteLine("введен не верный формат id");
                        }

                        if (_officeService.RemoveOffice(id))
                        {
                            Console.WriteLine("Удаление успешно! Нажмите Enter!");
                            Console.ReadLine();
                            flag = false;
                        }
                        else
                        {
                            Console.WriteLine("Введен не верный id");
                        }
                        break;
                    case "3":
                        Console.Clear();
                        _officeService.ShowOffices(listOffices);
                        Console.WriteLine("Нажмите Enter чтобы вернутся");
                        Console.ReadLine();
                        break;
                    case "4":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Такого действия нет");
                        break;
                }
            }
        }

        public void EmployeeServices()
        {
            var flag = true;
            while (flag)
            {
                Console.Clear();
                Console.WriteLine("Выбирите действие: " +
                        "\n1) Добавить сотрудника " +
                        "\n2) Удалить сотрудника " +
                        "\n3) Показать все сотрудников " +
                        "\n4) Сменить сотруднику тип и размер оплаты " +
                        "\n5) Добавить отработанные часы за текущий месяц сотруднику" +
                        "\n6) Расчитать зарплату всем работникам за текущий месяц" +
                        "\n7) Назад");
                string point = Console.ReadLine();
                var listEmployee = _employeeService.GetEmployee();
                switch (point)
                {
                    case "1":
                        try
                        {
                            AddEmployee();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case "2":
                        _employeeService.ShowEmployees(listEmployee);
                        int employeeId;
                        while (!int.TryParse(Console.ReadLine(), out employeeId) || !(listEmployee.Any(u => u.Id == employeeId)))
                        {
                            Console.WriteLine("введен не верный формат id");
                        }

                        if (_employeeService.RemoveEmployee(employeeId))
                        {
                            Console.WriteLine("Удаление успешно! Нажмите Enter!");
                            Console.ReadLine();
                            flag = false;
                        }
                        else
                        {
                            Console.WriteLine("Введен не верный id");
                        }
                        break;
                    case "3":
                        Console.Clear();
                        _employeeService.ShowEmployees(listEmployee);
                        Console.WriteLine("Нажмите Enter чтобы вернутся");
                        Console.ReadLine();
                        break;
                    case "4":
                        ChangeEmployeeSalaryType(listEmployee);
                        flag = false;
                        break;
                    case "5":
                        _employeeService.ShowEmployees(listEmployee);
                        Console.WriteLine("Введите id: ");
                        while (!int.TryParse(Console.ReadLine(), out employeeId) || !(listEmployee.Any(u => u.Id == employeeId)))
                        {
                            Console.WriteLine("введен не верный формат id");
                        }
                        int hours;
                        Console.WriteLine("Введите кол-во часов: ");
                        while (!int.TryParse(Console.ReadLine(), out hours) || !(hours > 0 || hours < 150))
                        {
                            Console.WriteLine("Введен не верный формат часов");
                        }

                        var hourForEmployee = new HourPerMonthDto
                        {
                            Employee = employeeId,

                            DateTime = DateTime.Now,

                            Hour = hours
                        };

                        _salaryService.AddHours(hourForEmployee);

                        flag = false;
                        break;
                    case "6":
                        _salaryService.AddSalaryForMonth();
                        flag = false;
                        break;
                    case "7":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Такого действия нет");
                        break;
                }
            }
        }

        private void ChangeEmployeeSalaryType(List<EmployeeDto> listEmployee)
        {
            int id;
            _employeeService.ShowEmployees(listEmployee);
            Console.WriteLine("Введите id сотрудника: ");
            while (!int.TryParse(Console.ReadLine(), out id) || !(listEmployee.Any(u => u.Id == id)))
            {
                Console.WriteLine("Введен не верный формат id");
            }

            var type = SalaryTypeSelected();
            if (type == SalaryType.Fixed)
            {
                Console.WriteLine("Введите оплату в месяц");
            }
            else if (type == SalaryType.Hourly)
            {
                Console.WriteLine("Введите оплату в час");
            }
            var paymentCount = PaymentCount();
            _employeeService.ChangeSalaryType(id, paymentCount);
            Console.WriteLine("Изменено успешно! Нажмите Enter!");
            Console.ReadLine();
            
        }

        private void AddEmployee()
        {
            var employee = new EmployeeDto();

            employee.OfficeId = OfficeIdService();

            employee.DepartmentId = DepartmentIdService();
            
            Console.WriteLine("Введите фамилию");
            employee.Surname = Console.ReadLine();

            Console.WriteLine("Введите имя");
            employee.Name = Console.ReadLine();

            Console.WriteLine("Введите отчество");
            employee.MiddleName = Console.ReadLine();

            Console.WriteLine("Введите должность");
            employee.PostName = Console.ReadLine();

            var type = SalaryTypeSelected();
            if (type == SalaryType.Fixed)
            {
                Console.WriteLine("Введите оплату в месяц");
            }
            else if (type == SalaryType.Hourly)
            {
                Console.WriteLine("Введите оплату в час");
            }
            employee.SalaryType = type;
            employee.Payment = PaymentCount();

            _employeeService.AddEmployee(employee);

        }

        public decimal PaymentCount()
        {
            decimal money;
            while (!decimal.TryParse(Console.ReadLine(), out money) )
            {
                Console.WriteLine("Введен не верный формат оплаты");
            }
            return money;
        }

        public SalaryType SalaryTypeSelected()
        {
            Console.WriteLine(" Выберите тип оплаты");
            Console.WriteLine(" 1)Фиксированная");
            Console.WriteLine(" 2)Почасовая");
            int id;
            while (!int.TryParse(Console.ReadLine(), out  id) || !(id == 1 || id == 2))
            {
                Console.WriteLine("введен не верный формат id");
            }

            return (SalaryType)id;
        }

        public int OfficeIdService()
        {
            var listOffices = _officeService.GetOffices();
            _officeService.ShowOffices(listOffices);
            int id;
            while (!int.TryParse(Console.ReadLine(), out id) || !(listOffices.Any(u => u.Id == id)))
            {
                Console.WriteLine("введен не верный формат id");
            }
            return id;
        }

        public int DepartmentIdService()
        {
            var listDepartaments = _departmentService.GetDepartments();
            _departmentService.ShowDepartments(listDepartaments);
            int id;
            while (!int.TryParse(Console.ReadLine(), out id) || !(listDepartaments.Any(u => u.Id == id)))
            {
                Console.WriteLine("введен не верный формат id");
            }
            return id;
        }
    }
}
