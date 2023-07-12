using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.WorkHours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Employees
{
    public class EmployeesData : IEmployeesData
    {

        private readonly ClinicDBContext _context;
        private readonly IWorkHoursData _iWorkHoursData;

        public EmployeesData(ClinicDBContext context, IWorkHoursData workHoursData)
        {
            _context = context;
            _iWorkHoursData = workHoursData;
        }

        public async Task<bool> CreateEmployee(Employee employee)
        {
            var isExsists = EmployeeExists(employee.Idemployee);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(employee);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public bool EmployeeExists(int id)
        {
            var employee = _context.Employees.Where(e => e.Idemployee == id).FirstOrDefault();
            if (employee == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<List<Employee>> GetAllEmployeesForRoom(List<string?> treatmentsType)
        {
            var employees = await GetAllEmployees();
            var list = new List<Employee>();
            foreach (var employee in employees)
            {
                if(employee == null)
                {
                    continue;
                }
                if(employee.Laser == true && treatmentsType != null && treatmentsType.Contains("Laser"))
                {
                    list.Add(employee);
                    continue;
                }
                if (employee.Waxing == true && treatmentsType != null && treatmentsType.Contains("Waxing"))
                {
                    list.Add(employee);
                    continue;
                }
                if (employee.Electrolysis == true && treatmentsType != null && treatmentsType.Contains("Electrolysis"))
                {
                    list.Add(employee);
                    continue;
                }
            }
            return list;
        }

        public async Task<string?> GetColorById(int id)
        {
            var employee = await _context.Employees.Where(e => e.Idemployee == id).FirstOrDefaultAsync();
            return employee?.Color;
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return employee;
        }

        public async Task<List<EmployeeDto>> GetEmployeesForSchedule(List<Employee> employees)
        {
            var list = new List<EmployeeDto>();
            int i = 0;
            foreach(var e in employees)
            {
                var emp = new EmployeeDto();
                emp.Id = i++;
                emp.IdWorker = e.Idemployee;
                emp.nameWorker = e.Name;
                emp.colorWorker = e.Color;
                emp.weeklyHouers = await _iWorkHoursData.GetWorkHourByEmployee(e.Idemployee);
                list.Add(emp);
            }
            return list;
        }
    }
}
