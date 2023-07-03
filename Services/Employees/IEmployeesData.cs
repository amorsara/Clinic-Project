using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Employees
{
    public interface IEmployeesData
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployeeById(int id);
        Task<bool> CreateEmployee(Employee employee);
        bool EmployeeExists(int id);
    }
}
