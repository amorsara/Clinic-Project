using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
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

        public TempCloseEmployeesData(ClinicDBContext context)
        {
            _context = context;
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
                list.Add(tempcloseemployee);
            }
            return list;
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

        public async Task<bool> UpdateTempcloseemployee(int id, TempCloseEmployeeDto tempcloseemployeedto)
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

            _context.Entry(tempcloseemployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempcloseemployeeExists(id))
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
