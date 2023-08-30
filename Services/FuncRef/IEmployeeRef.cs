using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FuncRef
{
    public interface IEmployeeRef
    {
        Task<List<Employee>> GetAllEmployeesForRoom(List<string>? treatmentsType);

        Task<string?> GetColorById(int id);

        Task<Employee?> GetEmployeeById(int id);

        Task<List<EmployeeDto>> GetEmployeesForSchedule(List<Employee> employees, bool regular, int idRoom);

        Task<int?> GetIdForAdmin();
    }
}
