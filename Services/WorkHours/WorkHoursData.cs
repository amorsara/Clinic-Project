using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.CloseRooms;
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
            if(workHour.Regularwork == true)
            {
                var res = await Check(workHour);
                if (res == false)
                {
                    return res;
                }
            }       
            await _context.AddAsync(workHour);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
                //var checkOk = await CheckAndUpdate(workHour);
                //return checkOk;
            }
            return false;
        }

        public async Task<bool> Check(Workhour workhour)
        {
            var shift = await _context.Workhours.Where(w => w.Idemployee == workhour.Idemployee && w.Day == workhour.Day && w.Shift == workhour.Shift && w.Regularwork == workhour.Regularwork).FirstOrDefaultAsync();
            if(shift == null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Workhour>> GetAllWorkHours()
        {
            return await _context.Workhours.ToListAsync();
        }

        public async Task<List<Workhour>> GetShiftByDay(int idRoom, int id, int day)
        {
            var shifts = await _context.Workhours.Where(w => w.Idemployee == id && w.Day == day && w.Idroom == idRoom).ToListAsync();
            return shifts;
        }

        public async Task<char?> GetShiftEmployee(int id, TimeOnly? time)
        {
            var res = await _context.Workhours.Where(w => w.Idemployee == id && w.Starthour <= time && w.Endhour >= time).FirstOrDefaultAsync();
            return res?.Shift;
        }

        public async Task<List<EmployeeShiftDto>> GetWorkHourByEmployee(int idRoom, int id, bool regular)
        {
            var list = new List<EmployeeShiftDto>();
            for (int i = 1; i < 7; i++)
            {
                var employeeShiftDto = new EmployeeShiftDto() { IdEmployeeShift = i};
                var shifts = await GetShiftByDay(idRoom,id, i);
                foreach (var s in shifts)
                {
                    if(s.Regularwork == !regular)
                    {
                        continue;
                    }
                    if(s == null)
                    {
                        continue;
                    }
                    if (s.Shift == 'm')
                    {
                        employeeShiftDto.startMorning = s.Starthour;
                        employeeShiftDto.endMorning = s.Endhour;
                    }
                    if (s.Shift == 'a')
                    {
                        employeeShiftDto.startAfternoon = s.Starthour;
                        employeeShiftDto.endAfternoon = s.Endhour;
                    }
                    if (s.Shift == 'e')
                    {
                        employeeShiftDto.startEvenning = s.Starthour;
                        employeeShiftDto.endEvenning = s.Endhour;
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

        public async Task<bool> DeleteShift(int id, int day, TimeOnly time)
        {
            var shift = _context.Workhours.Where(w => w.Idemployee == id && w.Day == day && w.Starthour == time).FirstOrDefault();
            var s = shift;
            if(s == null || shift == null)
            {
                return false;
            }
            var isOk = await DeleteWorkhour(shift.Idworkhour);
            //var isOk1 = await CheckAndUpdate(s);
            if(!isOk)
            {
                return false;
            }
            return true;
        }


        public async Task<bool> DeleteWorkhour(int id)
        {
            if (_context.Workhours == null)
            {
                return false;
            }
            var workhour = await _context.Workhours.FindAsync(id);
            if (workhour == null)
            {
                return false;
            }

            _context.Workhours.Remove(workhour);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateWorkhour(int id, Workhour workhour)
        {
            _context.Entry(workhour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkHourExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

    }
}
