using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TempCloseEmployees
{
    public class TempCloseEmployeesData : ITempCloseEmployeesData
    {
        private readonly ClinicDBContext _context;
        private readonly IEmployeesData _iEmployeesData;

        public TempCloseEmployeesData(ClinicDBContext context, IEmployeesData employeesData)
        {
            _context = context;
            _iEmployeesData = employeesData;
        }

        public async Task<bool> CreateTempcloseemployee(TempCloseEmployeeDto tempcloseemployeedto)
        {
            if (tempcloseemployeedto == null)
            {
                return false;
            }

            var isExsists = TempcloseemployeeExists(tempcloseemployeedto.id);
            if (isExsists)
            {
                return false;
            }

            var tempcloseemployee = new Tempcloseemployee();
            tempcloseemployee.Status = tempcloseemployeedto.status;
            tempcloseemployee.Startdate = tempcloseemployeedto.startDate;
            tempcloseemployee.Enddate = tempcloseemployeedto.endDate;
            tempcloseemployee.Starttime = tempcloseemployeedto.startTime;
            tempcloseemployee.Endtime = tempcloseemployeedto.endTime;
            tempcloseemployee.Idemployee = tempcloseemployeedto.idWorker;
            tempcloseemployee.Reason = tempcloseemployeedto.reason;

            await _context.AddAsync(tempcloseemployee);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTempcloseemployee(int id)
        {
            if (_context.Closerooms == null)
            {
                return false;
            }

            var tempcloseemployee = await GetTempcloseemployeeById(id);
            if (tempcloseemployee == null)
            {
                return false;
            }

            _context.Tempcloseemployees.Remove(tempcloseemployee);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<TempCloseEmployeeDto>> GetAllTempcloseemployees()
        {
            var employees =  await _context.Tempcloseemployees.ToListAsync();
            var list = new List<TempCloseEmployeeDto>();
            foreach(var emp in employees)
            {
                if(emp == null)
                {
                    continue;
                }
                var tempcloseemployee = new TempCloseEmployeeDto();
                tempcloseemployee.id = emp.Idtempcloseemployee;
                tempcloseemployee.startDate = emp.Startdate;
                tempcloseemployee.endDate = emp.Enddate;
                tempcloseemployee.startTime = emp.Starttime;
                tempcloseemployee.endTime = emp.Endtime;
                tempcloseemployee.idWorker = emp.Idemployee;
                tempcloseemployee.reason = emp.Reason;
                tempcloseemployee.status = emp.Status;
               
                list.Add(tempcloseemployee);
            }
            return list;
        }

        public async Task<List<TempCloseEmployeeDto>> GetAllTempcloseemployeesById(int id)
        {
            var employees = await _context.Tempcloseemployees.Where(t => t.Idemployee == id).ToListAsync();
            var list = new List<TempCloseEmployeeDto>();
            foreach (var emp in employees)
            {
                if (emp == null)
                {
                    continue;
                }
                var tempcloseemployee = new TempCloseEmployeeDto();
                tempcloseemployee.id = emp.Idtempcloseemployee;
                tempcloseemployee.startDate = emp.Startdate;
                tempcloseemployee.endDate = emp.Enddate;
                tempcloseemployee.startTime = emp.Starttime;
                tempcloseemployee.endTime = emp.Endtime;
                tempcloseemployee.idWorker = emp.Idemployee;
                tempcloseemployee.reason = emp.Reason;
                tempcloseemployee.status = emp.Status;
                list.Add(tempcloseemployee);
            }
            return list;
        }

        public async Task<List<CloseEventsDto>> GetCloseEventsForEmployees()
        {
            var employees = await _context.Tempcloseemployees.Where(e => e.Status == true).ToListAsync();
            var listEvent = new List<CloseEventsDto>();
            foreach(var emp in employees)
            {
                if(emp == null)
                {
                    continue;
                }

                var closeEvent = new CloseEventsDto();
                closeEvent.id = emp.Idtempcloseemployee;
                closeEvent.idRoom = 0; // fix....
                closeEvent.date = emp.Startdate;
                closeEvent.startHour = emp.Starttime;
                closeEvent.endTime = emp.Endtime;
                closeEvent.nameEvent = emp.Reason;

                var empl = await _iEmployeesData.GetEmployeeById(emp.Idemployee);
                if (empl != null)
                {
                    var e = new EmployeeDetails();
                    e.Id = emp.Idemployee;
                    e.Name = empl.Name;
                    e.Color = empl.Color;
                    closeEvent.employee = e;
                }

                listEvent.Add(closeEvent);
            }
            return listEvent;
        }

        public async Task<Tempcloseemployee?> GetTempcloseemployeeById(int id)
        {
            var tempcloseemployee = await _context.Tempcloseemployees.FindAsync(id);
            return tempcloseemployee;
        }

        public bool TempcloseemployeeExists(int id)
        {
            var tempcloseemployee = _context.Tempcloseemployees.Where(t => t.Idtempcloseemployee == id).FirstOrDefault();
            if (tempcloseemployee == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateStatusTempcloseemployee(int id, bool status)
        {
            var tempcloseemp = await GetTempcloseemployeeById(id);
            if(tempcloseemp == null)
            {
                return false;
            }

            tempcloseemp.Status = status;

            var isOk = await UpdateTempcloseemployee(tempcloseemp);
            return isOk;
        }

        public async Task<bool> UpdateTempcloseemployee(Tempcloseemployee tempcloseemployee)
        {
            _context.Entry(tempcloseemployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempcloseemployeeExists(tempcloseemployee.Idemployee))
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

        public async Task<bool> UpdateTempcloseemployeeWrapper(int id, TempCloseEmployeeDto tempcloseemployeedto)
        {

            if (tempcloseemployeedto == null)
            {
                return false;
            }

            var tempcloseemployee = await GetTempcloseemployeeById(id);
            if(tempcloseemployee == null)
            {
                return false;
            }

            tempcloseemployee.Startdate = tempcloseemployeedto.startDate;
            tempcloseemployee.Enddate = tempcloseemployeedto.endDate;
            tempcloseemployee.Starttime = tempcloseemployeedto.startTime;
            tempcloseemployee.Endtime = tempcloseemployeedto.endTime;
            tempcloseemployee.Idemployee = tempcloseemployeedto.idWorker;
            tempcloseemployee.Reason = tempcloseemployeedto.reason;
            tempcloseemployee.Status = tempcloseemployeedto.status;

            var isOk = await UpdateTempcloseemployee(tempcloseemployee);
            return isOk;
            
        }
    }
}
