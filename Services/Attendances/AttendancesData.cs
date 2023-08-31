using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Attendances
{
    public class AttendancesData : IAttendancesData
    {
        private readonly ClinicDBContext _context;
        private readonly IEmployeesData _iEmployeesData;

        public AttendancesData(ClinicDBContext context, IEmployeesData employeesData)
        {
            _context = context;
            _iEmployeesData = employeesData;
        }

        public bool AttendanceExists(int id)
        {
            var attendance = _context.Attendances.Where(a => a.Idattendance == id).FirstOrDefault();
            if (attendance == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CreateAttendance(Attendance attendance)
        {

            var isExsists = AttendanceExists(attendance.Idattendance);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(attendance);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAttendance(int id)
        {
            if (_context.Attendances == null)
            {
                return false;
            }

            var attendance = await GetAttendanceById(id);
            if(attendance == null)
            {
                return false;
            }

            var ok = await _context.Attendances.FindAsync(attendance.Idattendance);
            if (ok == null)
            {
                return false;
            }

            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExitAttendance(Attendance attendance)
        {
            var attend = await _context.Attendances.Where(a => a.Idemployee == attendance.Idemployee && a.Date == attendance.Date && a.Timeenter != null && a.Timeexit == null).FirstOrDefaultAsync();
            if(attend == null || attend.Timeexit != null) // לא הכניסו שעת כניסה...
            {
                var isOk = await CreateAttendance(attendance);
                return isOk;
            }
            else
            {
                attend.Timeexit = attendance.Timeexit;
                var isOk = await UpdateAttendance(attend.Idattendance, attend);
                return isOk;
            }
        }

        public async Task<List<AllAttendanceDto>> GetAllAttendances()
        {
            var allAttendances = await GetAttendances();
            var list = new List<AllAttendanceDto>();
            foreach (var attendance in allAttendances)
            {
                if(attendance == null)
                {
                    continue;
                }
                var attend = new AllAttendanceDto();
                attend.id = attendance.Idattendance;
                var emp = await _iEmployeesData.GetEmployeeById(attendance.Idemployee);
                if(emp != null)
                {
                    var employeeDetails = new EmployeeDetails();
                    employeeDetails.Id = emp.Idemployee;
                    employeeDetails.Name = emp.Name;
                    employeeDetails.Color = emp.Color;
                    attend.employee = employeeDetails;
                }
                
                attend.date = attendance.Date;
                attend.timeEnter = attendance.Timeenter;
                attend.timeExit = attendance.Timeexit;
                attend.sum = attendance.Timeexit - attendance.Timeenter;

                list.Add(attend);
            }
            return list;
        }

        public async Task<Attendance?> GetAttendanceById(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            return attendance;
        }

        public async Task<List<Attendance>> GetAttendances()
        {
            var waitings = await _context.Attendances.ToListAsync();
            return waitings;
        }

        public async Task<bool> UpdateAttendance(int id, Attendance attendance)
        {
            _context.Entry(attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
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
