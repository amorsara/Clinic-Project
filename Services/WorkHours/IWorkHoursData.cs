using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WorkHours
{
    public interface IWorkHoursData
    {
        Task<List<Workhour>> GetAllWorkHours();
        Task<Workhour?> GetWorkHourById(int id);
        Task<bool> CreateWorkHour(Workhour workHour);
        Task <int?> GetShiftEmployee(int id, TimeOnly? time);
        bool WorkHourExists(int id);
        Task<List<EmployeeShiftDto>> GetWorkHourByEmployee(int id);
        Task<List<Workhour>> GetShiftByDay(int id, int day);
    }
}
