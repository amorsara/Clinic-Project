using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FuncRef
{
    public interface IWorkHourRef
    {
        Task<char?> GetShiftEmployee(int id, TimeOnly? time);

        Task<List<EmployeeShiftDto>> GetWorkHourByEmployee(int idRoom, int id, bool regular);

        //Task<List<EmployeeShiftDto>> GetWorkHourByEmployeeForWeek(int idRoom, int id, DateOnly date);
    }
}
