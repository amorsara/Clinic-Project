using Repository.GeneratedModels;
using Services.DTO;
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
        Task<string?> GetColorById(int id);
        Task<int?> GetEmployeIdByName(string? name);
        Task<List<string>> GetAllNameEmployees();
        Task<bool> CreateEmployee(Employee employee);
        bool EmployeeExists(int id);
        Task<List<Employee>> GetAllEmployeesForRoom(List<String>? treatmentsType);
        Task<List<EmployeeDto>> GetEmployeesForSchedule(List<Employee> employees);
        Task<List<EmployeeFieldsDto>> GetAllEmployeesFields();
    }
}
