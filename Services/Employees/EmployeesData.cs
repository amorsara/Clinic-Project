using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
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

        public EmployeesData(ClinicDBContext context)
        {
            _context = context;
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

        public async Task<Employee?> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return employee;
        }
    }
}
