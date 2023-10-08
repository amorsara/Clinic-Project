using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Schedule
{
    public interface IScheduleData
    {
        Task<List<ScheduleDto>> GetAllDates(DateOnly? date = null);

        Task <List<RoomScheduleDto>> GetAllSchedules(bool regular);
       
    }
}
