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


//public async Task<List<EmployeeShiftDto>> GetWorkHourByEmployeeForWeek(int idRoom, int id, DateOnly date)
//{
//    var closerooms = await _iCloseRoomsData.GetAllCloseroomsForId(idRoom, date);
//    var list = new List<EmployeeShiftDto>();
//    for (int i = 1; i < 7; i++)
//    {
//        var employeeShiftDto = new EmployeeShiftDto() { IdEmployeeShift = i };
//        var shifts = await GetShiftByDay(idRoom, id, i);
//        foreach (var s in shifts)
//        {

//            if (s == null)
//            {
//                continue;
//            }
//            if (closerooms != null)
//            {
//                foreach (var d in closerooms)
//                {
//                    var day1 = d.Startdate?.DayOfWeek != null ? (int)d.Startdate?.DayOfWeek + 1 : 0;
//                    var day2 = d.Enddate?.DayOfWeek != null ? (int)d.Enddate?.DayOfWeek + 1 : 0;
//                    if (d != null && (day1 == i || day2 == i))
//                    {
//                        if (s.Shift == 'm' && (s.Starthour > d.Endtime || s.Endhour < d.Starttime))
//                        {
//                            employeeShiftDto.startMorning = s.Starthour;
//                            employeeShiftDto.endMorning = s.Endhour;
//                        }
//                        if (s.Shift == 'a' && (s.Starthour > d.Endtime || s.Endhour < d.Starttime))
//                        {
//                            employeeShiftDto.startAfternoon = s.Starthour;
//                            employeeShiftDto.endAfternoon = s.Endhour;
//                        }
//                        if (s.Shift == 'e' && (s.Starthour > d.Endtime || s.Endhour < d.Starttime))
//                        {
//                            employeeShiftDto.startEvenning = s.Starthour;
//                            employeeShiftDto.endEvenning = s.Endhour;
//                        }
//                    }
//                }
//            }
//            else
//            {
//                if (s.Shift == 'm')
//                {
//                    employeeShiftDto.startMorning = s.Starthour;
//                    employeeShiftDto.endMorning = s.Endhour;
//                }
//                if (s.Shift == 'a')
//                {
//                    employeeShiftDto.startAfternoon = s.Starthour;
//                    employeeShiftDto.endAfternoon = s.Endhour;
//                }
//                if (s.Shift == 'e')
//                {
//                    employeeShiftDto.startEvenning = s.Starthour;
//                    employeeShiftDto.endEvenning = s.Endhour;
//                }
//            }
//        }
//        list.Add(employeeShiftDto);
//    }
//    return list;
//}

//private readonly ICloseRoomsData _iCloseRoomsData;
//public WorkHoursData(ClinicDBContext context, ICloseRoomsData closeRoomsData)
//{
//    _context = context;
//    _iCloseRoomsData = closeRoomsData; // שגיאה
//}




//public async Task<bool> CheckAndUpdate(Workhour workhour)
//{
//    var shifts = await _context.Workhours.Where(w => w.Idemployee == workhour.Idemployee && w.Day == workhour.Day && w.Regularwork == workhour.Regularwork).ToListAsync();

//    bool updateOk1 = true, updateOk2 = true, updateOk3 = true;

//    shifts = shifts.OrderBy(shift => shift.Starthour).ToList();
//    if(shifts.Count >= 1)
//    {
//        var m = shifts[0];
//        m.Shift = 'm';
//        updateOk1 = await UpdateWorkhour(m.Idworkhour, m);
//    }
//    if (shifts.Count >= 2)
//    {
//        var a = shifts[1];
//        a.Shift = 'a';
//        updateOk2 = await UpdateWorkhour(a.Idworkhour, a);
//    }
//    if (shifts.Count >= 3)
//    {
//        var e = shifts[2];
//        e.Shift = 'e';
//        updateOk3 = await UpdateWorkhour(e.Idworkhour, e);
//    }
//    if(!updateOk1 || !updateOk2 || !updateOk3)
//    {
//        return false;
//    }

//    return true;
//}


//if (shifts.Count == 0) // the first shift...
//{
//    workhour.Shift = 'm';
//    var updateOk1 = await UpdateWorkhour(workhour.Idworkhour, workhour);
//    return updateOk1;
//}
//else
//{
//    if (shifts.Count == 1) // there is one shift in m - need check the time...
//    {
//        if (shifts[0].Starthour <= workhour.Starthour)
//        {
//            workhour.Shift = 'a';
//            var updateOk1 = await UpdateWorkhour(workhour.Idworkhour, workhour);
//            return updateOk1;
//        }
//        else // need to update the shift - m=>a & a=>m
//        {
//            workhour.Shift = 'm';
//            var updateOk1 = await UpdateWorkhour(workhour.Idworkhour, workhour);
//            shifts[0].Shift = 'a';
//            var updateOk2 = await UpdateWorkhour(workhour.Idworkhour, workhour);
//            if (!updateOk1 || !updateOk2)
//            {
//                return false;
//            }
//        }
//    }
//}


//public async Task<bool> CheckAndUpdate(Workhour workhour)
//{
//    var shifts = _context.Workhours.Where(w => w.Idemployee == workhour.Idemployee && w.Day == workhour.Day && w.Regularwork == workhour.Regularwork).ToList();
//    var shiftM = shifts.Where(s => s.Shift == 'm').FirstOrDefault();
//    var shiftA = shifts.Where(s => s.Shift == 'a' && workhour.Idworkhour != s.Idworkhour).FirstOrDefault();
//    if (shiftM == null)
//    {
//        return false;
//    }
//    if (workhour.Shift == 'a' && workhour.Starthour < shiftM.Starthour)
//    {
//        // update a=>m m=>a
//        workhour.Shift = 'm';
//        var updateOk1 = await UpdateWorkhour(workhour.Idworkhour, workhour);
//        shiftM.Shift = 'a';
//        var updateOk2 = await UpdateWorkhour(shiftM.Idworkhour, shiftM);
//        if (!updateOk1 || !updateOk2)
//        {
//            return false;
//        }
//    }
//    else
//    {
//        if (shiftA == null && workhour.Shift == 'e')
//        {
//            return false;
//        }
//        if (shiftM != null && shiftA != null && workhour.Starthour <= shiftM.Starthour)
//        {
//            // update e=>m m=>a a=>e
//            workhour.Shift = 'm';
//            var updateOk1 = await UpdateWorkhour(workhour.Idworkhour, workhour);
//            shiftM.Shift = 'a';
//            var updateOk2 = await UpdateWorkhour(shiftM.Idworkhour, shiftM);
//            shiftA.Shift = 'e';
//            var updateOk3 = await UpdateWorkhour(shiftA.Idworkhour, shiftA);
//            if (!updateOk1 || !updateOk2 || !updateOk3)
//            {
//                return false;
//            }
//        }
//        if (shiftA != null && workhour.Starthour <= shiftA.Starthour)
//        {
//            // update e=>a a=>e
//            workhour.Shift = 'a';
//            var updateOk1 = await UpdateWorkhour(workhour.Idworkhour, workhour);
//            shiftA.Shift = 'e';
//            var updateOk3 = await UpdateWorkhour(shiftA.Idworkhour, shiftA);
//            if (!updateOk1 || !updateOk3)
//            {
//                return false;
//            }
//        }
//    }
//    return true;
//}



//public async Task<bool> DeleteShift(int id, int day, TimeOnly time)
//{
//    var shift = _context.Workhours.Where(w => w.Idemployee == id && w.Day == day && w.Starthour == time).FirstOrDefault();
//    if (shift == null)
//    {
//        return false;
//    }
//    var isOk = await DeleteWorkhour(shift.Idworkhour);
//    if (shift.Shift == 'e') // last shift...
//    {
//        return isOk;
//    }
//    else
//    {
//        if (shift.Shift == 'a') // mid shift and check if this shift is last...
//        {
//            var s = _context.Workhours.Where(w => w.Idemployee == id && w.Day == day && w.Shift == 'e').FirstOrDefault();
//            if (s != null) // not last, need to update the shift - e...
//            {
//                s.Shift = 'a';
//                var okUpdate = await UpdateWorkhour(s.Idworkhour, s);
//            }
//            return isOk;
//        }
//        else // this shift is - m
//        {
//            var sh = _context.Workhours.Where(w => w.Idemployee == id && w.Day == day).ToList();
//            if (sh != null)
//            {
//                foreach (var item in sh)
//                {
//                    if (item.Shift == 'a')
//                    {
//                        item.Shift = 'm';
//                        var okUpdate = await UpdateWorkhour(item.Idworkhour, item);
//                    }
//                    else
//                    {
//                        item.Shift = 'a';
//                        var okUpdate = await UpdateWorkhour(item.Idworkhour, item);
//                    }
//                }
//            }
//        }
//    }
//    return isOk;
//}
