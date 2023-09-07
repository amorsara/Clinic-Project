using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Appointments;
using Services.DTO;
using Services.Employees;
using Services.Rooms;
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
        private readonly IRoomsData _roomsData;
        private readonly IAppointmentsData _iAppointmentsData;

        public TempCloseEmployeesData(ClinicDBContext context, IEmployeesData employeesData, IRoomsData roomsData, IAppointmentsData appointmentsData)
        {
            _context = context;
            _iEmployeesData = employeesData;
            _roomsData = roomsData;
            _iAppointmentsData = appointmentsData;
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

            var cancel = await _iAppointmentsData.CancelAppointment(0, tempcloseemployee.Idemployee, tempcloseemployee.Startdate, tempcloseemployee.Enddate, tempcloseemployee.Starttime, tempcloseemployee.Endtime, false);
            if (cancel == false)
            {
                return false;
            }

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

                if (emp.Startdate != null && emp.Enddate != null)
                {
                    var d1 = (DateOnly)emp.Startdate;
                    var d2 = (DateOnly)emp.Enddate;
                    while (d1 <= d2)
                    {
                        var list = await _roomsData.GetAllRoomsIdForEmployee(emp.Idemployee);
                        if(list != null)
                        {
                            foreach (var id in list)
                            {
                                var closeEvent = new CloseEventsDto();
                                closeEvent.id = emp.Idtempcloseemployee;
                                closeEvent.idRoom = id;
                                closeEvent.date = d1;
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
                        }
                        d1 = d1.AddDays(1);
                    }
                }    
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
             
            if (isOk && status) // need to cancel appointmets...
            {
                var cancel = await _iAppointmentsData.CancelAppointment(0, id, tempcloseemp.Startdate, tempcloseemp.Enddate, tempcloseemp.Starttime, tempcloseemp.Endtime, true);
                if (cancel == false)
                {
                    return false;
                }
            }

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
