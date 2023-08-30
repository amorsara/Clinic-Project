using Repository.GeneratedModels;
using Services.DTO;
using Services.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FuncRef
{
    public class EmployeeRef : IEmployeeRef
    {
        private readonly IEmployeesData _iEmployeesData;


        public EmployeeRef(IEmployeesData employeesData)
        {
            _iEmployeesData = employeesData;
        }

        public async Task<List<Employee>> GetAllEmployeesForRoom(List<string>? treatmentsType)
        {
            return await _iEmployeesData.GetAllEmployeesForRoom(treatmentsType);
        }

        public async Task<string?> GetColorById(int id)
        {
            return await _iEmployeesData.GetColorById(id);
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            return await _iEmployeesData.GetEmployeeById(id);
        }

        public async Task<List<EmployeeDto>> GetEmployeesForSchedule(List<Employee> employees, bool regular, int idRoom)
        {
            return await _iEmployeesData.GetEmployeesForSchedule(employees, regular, idRoom);
        }

        public async Task<int?> GetIdForAdmin()
        {
            return await _iEmployeesData.GetIdForAdmin();
        }
    }
}
