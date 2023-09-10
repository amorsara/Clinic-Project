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

        Task<int?> GetIdForAdmin();

        Task<string?> GetColorById(int id);

        Task<List<String>?> GetAllTreatmentsForEmployee(int id);

        Task<Employee?> GetEmployeeByName(string? name);

        Task<List<EmployeeDetails>> GetEmployeeDetails();

        Task<int?> GetEmployeIdByName(string? name);

        Task<List<string>> GetAllNameEmployees();

        Task<bool> CreateEmployee(Employee employee);

        bool EmployeeExists(int id);

        Task<List<Employee>> GetAllEmployeesForRoom(List<String>? treatmentsType);

        Task<List<EmployeeDto>> GetEmployeesForSchedule(List<Employee> employees, bool regular, int idRoom);

        //Task<List<EmployeeDto>> GetEmployeesForScheduleForWeek(List<Employee> employees, DateOnly date, int idRoom);

        Task<List<EmployeeFieldsDto>> GetAllEmployeesFields();

        Task<bool> UpdateEmployee(int id, Employee employee);

        Task<bool> UpdateNameEmployee(int id, string name);

        Task<bool> ChangeEmployees(List<List<RoomEmployeeDto>> employees);

        Task<bool>DeleteEmployeeById(int id);

        Task<List<List<RoomEmployeeDto>>> GetAllEmployeesWithTypes();
    }
}
