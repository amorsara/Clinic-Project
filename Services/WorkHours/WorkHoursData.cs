using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WorkHours
{
    public class WorkHoursData : IWorkHoursData
    {

        private readonly ClinicDBContext _context;

        public WorkHoursData(ClinicDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateWorkHour(Workhour workHour)
        {
            var isExsists = WorkHourExists(workHour.Idworkhour);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(workHour);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Workhour>> GetAllWorkHours()
        {
            return await _context.Workhours.ToListAsync();
        }

        public async Task<List<Workhour>> GetShiftByDay(int id, int day)
        {
            var shifts = await _context.Workhours.Where(w => w.Idemployee == id && w.Day == day).ToListAsync();
            return shifts;
        }

        public async Task<int?> GetShiftEmployee(int id, TimeOnly? time)
        {
            var res = await _context.Workhours.Where(w => w.Idemployee == id && w.Starthour <= time).FirstOrDefaultAsync();
            return res?.Shift;
        }

        public async Task<List<EmployeeShiftDto>> GetWorkHourByEmployee(int id)
        {
            var list = new List<EmployeeShiftDto>();
            for (int i = 1; i < 7; i++)
            {
                var employeeShiftDto = new EmployeeShiftDto() { IdEmployeeShift = i};
                var shifts = await GetShiftByDay(id, i);
                foreach (var s in shifts)
                {
                    if(s == null)
                    {
                        continue;
                    }
                    if (s.Shift == 1)
                    {
                        employeeShiftDto.StartMorning = s.Starthour;
                        employeeShiftDto.EndMorning = s.Endhour;
                    }
                    if (s.Shift == 2)
                    {
                        employeeShiftDto.StartAfternoon = s.Starthour;
                        employeeShiftDto.EndAfternoon = s.Endhour;
                    }
                    if (s.Shift == 3)
                    {
                        employeeShiftDto.StartEvenning = s.Starthour;
                        employeeShiftDto.EndEvenning = s.Endhour;
                    }
                }
                list.Add(employeeShiftDto);
            }
            return list;
        }

        public async Task<Workhour?> GetWorkHourById(int id)
        {
            var workHour = await _context.Workhours.FindAsync(id);
            return workHour;
        }

        public bool WorkHourExists(int id)
        {
            var workHour = _context.Workhours.Where(w => w.Idworkhour == id).FirstOrDefault();
            if (workHour == null)
            {
                return false;
            }
            return true;
        }
    }
}
