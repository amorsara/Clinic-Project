using Repository.GeneratedModels;
using Services.DTO;
using Services.WorkHours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FuncRef
{
    public class WorkHourRef : IWorkHourRef
    {
        private readonly IWorkHoursData _iWorkHoursData;

        public WorkHourRef(IWorkHoursData workHoursData)
        {
            _iWorkHoursData = workHoursData;
        }

        public async Task<char?> GetShiftEmployee(int id, TimeOnly? time)
        {
            return await _iWorkHoursData.GetShiftEmployee(id, time);    
        }

        public async Task<List<EmployeeShiftDto>> GetWorkHourByEmployee(int idRoom, int id, bool regular)
        {
            return await _iWorkHoursData.GetWorkHourByEmployee(idRoom, id, regular);
        }

        //public async Task<List<EmployeeShiftDto>> GetWorkHourByEmployeeForWeek(int idRoom, int id, DateOnly date)
        //{
        //    return await _iWorkHoursData.GetWorkHourByEmployeeForWeek(idRoom, id, date);
        //}
    }
}
