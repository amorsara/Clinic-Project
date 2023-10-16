using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Attendances
{
    public interface IAttendancesData
    {
        Task<List<Attendance>> GetAttendances();

        Task<List<AllAttendanceDto>> GetAllAttendances(bool r);

        Task<Attendance?> GetAttendanceById(int id);

        Task<bool> UpdateAttendance(int id, Attendance attendance);

        Task<bool> CreateAttendance(Attendance attendance);

        Task<bool> ExitAttendance(Attendance attendance);

        Task<bool> DeleteAttendance(int id);

        bool AttendanceExists(int id);

    }
}
