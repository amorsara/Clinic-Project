using Repository.GeneratedModels;
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
        bool WorkHourExists(int id);
    }
}
