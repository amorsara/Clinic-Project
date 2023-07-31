using Microsoft.AspNetCore.Mvc;
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
                if(workHour.Shift != 'm') // if have other shift...
                {
                    var checkOk = await CheckAndUpdate(workHour);
                    return checkOk;
                }
                
                return true;
            }
            return false;
        }

        public async Task<bool> CheckAndUpdate(Workhour workhour)
        {
            var shifts = _context.Workhours.Where(w => w.Idemployee == workhour.Idemployee && w.Day == workhour.Day && w.Regularwork == workhour.Regularwork).ToList();
            var shiftM = shifts.Where(s => s.Shift == 'm').FirstOrDefault();
            var shiftA = shifts.Where(s => s.Shift == 'a').FirstOrDefault();
            if(shiftM == null)
            {
                return false;
            }
            if(workhour.Shift == 'a' && workhour.Starthour < shiftM.Starthour)
            {
                // update a=>m m=>a
                workhour.Shift = 'm';
                var updateOk1 = await UpdateWorkhour(workhour.Idworkhour, workhour);
                shiftM.Shift = 'a';
                var updateOk2 = await UpdateWorkhour(shiftM.Idworkhour, shiftM);
                if (!updateOk1 || !updateOk2)
                {
                    return false;
                }
            }
            else
            {
                if(shiftA == null)
                {
                    return false;
                }
                if (shiftM != null && workhour.Starthour <= shiftM.Starthour)
                {
                    // update e=>m m=>a a=>e
                    workhour.Shift = 'm';
                    var updateOk1 = await UpdateWorkhour(workhour.Idworkhour, workhour);
                    shiftM.Shift = 'a';
                    var updateOk2 = await UpdateWorkhour(shiftM.Idworkhour, shiftM);
                    shiftA.Shift = 'e';
                    var updateOk3 = await UpdateWorkhour(shiftA.Idworkhour, shiftA);
                    if (!updateOk1 || !updateOk2 || !updateOk3)
                    {
                        return false;
                    }
                }
                if (shiftA != null && workhour.Starthour <= shiftA.Starthour)
                {
                    // update e=>a a=>e
                    workhour.Shift = 'a';
                    var updateOk1 = await UpdateWorkhour(workhour.Idworkhour, workhour);
                    shiftA.Shift = 'e';
                    var updateOk3 = await UpdateWorkhour(shiftA.Idworkhour, shiftA);
                    if (!updateOk1 || !updateOk3)
                    {
                        return false;
                    }
                }
            }
            return true;
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

        public async Task<char?> GetShiftEmployee(int id, TimeOnly? time)
        {
            var res = await _context.Workhours.Where(w => w.Idemployee == id && w.Starthour <= time && w.Endhour >= time).FirstOrDefaultAsync();
            return res?.Shift;
        }

        public async Task<List<EmployeeShiftDto>> GetWorkHourByEmployee(int id, bool regular)
        {
            var list = new List<EmployeeShiftDto>();
            for (int i = 1; i < 7; i++)
            {
                var employeeShiftDto = new EmployeeShiftDto() { IdEmployeeShift = i};
                var shifts = await GetShiftByDay(id, i);
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
            if(shift == null)
            {
                return false;
            }
            var isOk = await DeleteWorkhour(shift.Idworkhour);
            if (shift.Shift == 'e') // last shift...
            {
                return isOk;
            }
            else
            {
                if(shift.Shift == 'a') // mid shift and check if this shift is last...
                {
                    var s = _context.Workhours.Where(w => w.Idemployee == id && w.Day == day && w.Shift == 'e').FirstOrDefault();
                    if(s != null) // not last, need to update the shift - e...
                    {
                        s.Shift = 'a';
                        var okUpdate = await UpdateWorkhour(s.Idworkhour, s);
                    }
                    return isOk;
                }
                else // this shift is - m
                {
                    var sh = _context.Workhours.Where(w => w.Idemployee == id && w.Day == day).ToList();
                    if(sh != null)
                    {
                        foreach(var item in sh)
                        {
                            if(item.Shift == 'a')
                            {
                                item.Shift = 'm';
                                var okUpdate = await UpdateWorkhour(item.Idworkhour, item);
                            }
                            else
                            {
                                item.Shift = 'a';
                                var okUpdate = await UpdateWorkhour(item.Idworkhour, item);
                            }
                        }
                    }
                }
            }
            return isOk;
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
