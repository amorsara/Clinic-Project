using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Contacts;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Appointments
{
    public class AppointmentsData : IAppointmentsData
    {
        private readonly ClinicDBContext _context;

        public AppointmentsData(ClinicDBContext context)
        {
            _context = context;
        }

        public bool AppointmentExists(int id)
        {
            var appointments = _context.Appointments.Where(a => a.Idappointment == id).FirstOrDefault();
            if (appointments == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CancelAppointment(int idRoom, int idEmployee, DateOnly? sDate, DateOnly? eDate, TimeOnly? sTime, TimeOnly? eTime,bool cancel)
        {
            var appointments = await GetAllAppointments();
            var flag = true;
            var toCancel = false;
            foreach (var appointment in appointments)
            {
                toCancel = false;

                if (appointment == null)
                {
                    continue;
                }
                
               //if(appointment.Idappointment == 911)
               // {
               //     DateOnly dd = new DateOnly(2023, 08, 01);
               // }
                if(appointment.Date >= sDate && appointment.Date <= eDate && (idRoom > 0 && appointment.Idroom == idRoom || idEmployee > 0 && appointment.Idemployee == idEmployee)) 
                {
                    if (appointment.Timestart <= sTime && eTime <= appointment.Timeend)
                    {
                        toCancel = true;
                    }

                    if (appointment.Timestart < sTime && sTime < appointment.Timeend)
                    {
                        toCancel = true;
                    }

                    if (sTime < appointment.Timestart && appointment.Timeend < eTime)
                    {
                        toCancel = true;
                    }

                    if (sTime < appointment.Timestart && appointment.Timestart < eTime)
                    {
                        toCancel = true;
                    }

                    if (toCancel == true)
                    {
                        appointment.Cancle = cancel;
                        var isOk = await UpdateAppointment(appointment.Idappointment, appointment);
                        if(isOk == false)
                        {
                            flag = false;
                        }
                    }                                      
                }
            }
            return flag;
        }

        public async Task<bool> CreateAppointment(Appointment appointments)
        {
            appointments.Cancle = false;
            appointments.Ispay = false;
            var isExsists = AppointmentExists(appointments.Idappointment);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(appointments);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await _context.Appointments.OrderBy(a => a.Date).ToListAsync();
        }

        public async Task<List<Appointment>> GetAllAppointmentsForWeek(DateOnly? date = null)
        {
            if (date == null)
            {
                return await GetAllAppointments();
            }

            var date2 = (DateOnly)date;
            return await _context.Appointments.Where(a => a.Date >= date && a.Date <= date2.AddDays(5)).ToListAsync();
        }

        public async Task<List<AppointmentScheduleDto>> GetAllAppointmentDataForWeek(DateOnly date)
        {
            var date2 = (DateOnly)date;

            // Query the database to get the required data
            var appointmentsData = await _context.Appointments
                .Where(a => a.Date >= date && a.Date <= date2.AddDays(5))
                .Select(a => new AppointmentScheduleDto
                {
                    Idappointment = a.Idappointment,
                    Idcontact = a.Idcontact,
                    Timestart = a.Timestart,
                    Timeend = a.Timeend,
                    Date = a.Date,
                    Treatmentname = a.Treatmentname,
                    Isremaind = a.Isremaind,
                    Cancle = a.Cancle,
                    Idemployee = a.Idemployee,
                    Color = a.IdemployeeNavigation.Color,
                    RoomName = a.Room.Name,
                    Shift = a.IdemployeeNavigation.Workhours
                        .FirstOrDefault(w => w.Idemployee == id && w.Starthour <= a.Timestart && w.Endhour >= a.Timestart)
                        .Shift,
                    Firstname = a.IdcontactNavigation.Firstname,
                    Lastname = a.IdcontactNavigation.Lastname,
                    Remark = a.Remark,
                    Phonenumber1 = a.IdcontactNavigation.Phonenumber1,
                    Phonenumber2 = a.IdcontactNavigation.Phonenumber2,
                    Phonenumber3 = a.IdcontactNavigation.Phonenumber3,
                    Duration = (int)a.Duration,
                    Area = a.Area,
                    Ispay = (bool)a.Ispay
                })
                .ToListAsync();
            return appointmentsData;
        }

        public async Task<List<DateOnly>> GetAllFutureDatesById(int id)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            var dates = await _context.Appointments.Where(a => a.Idcontact == id && a.Date >= today).ToListAsync();
            var list = new List<DateOnly>();
            foreach(var date in dates)
            {
                if(date.Date != null)
                {
                    list.Add((DateOnly)date.Date);
                }                
            }
            return list;
        }

        public async Task<List<Appointment>> GetAllWaitDates()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            var appointments = await _context.Appointments.Where(a => a.Date >= today).ToListAsync();
            return appointments;
        }

        public async Task<Appointment?> GetAppointmentById(int id)
        {
            var appointments = await _context.Appointments.FindAsync(id);
            return appointments;
        }

        public async Task<List<FutureDateDto>> GetDatesOfAppointments(int id)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            var appointments = await _context.Appointments.Where(a => a.Idcontact == id && a.Date >= today).ToListAsync();
            var listDate = new List<FutureDateDto>();
            foreach(var appointment in appointments)
            {
                if(appointment.Date != null)
                {
                    var d = new FutureDateDto();
                    d.date = appointment.Date;
                    d.startHour = appointment.Timestart;
                    d.endTime = appointment.Timeend;
                    d.treatment = appointment.Treatmentname;
                    listDate.Add(d);

                }
            }
            return listDate;
        }

        public async Task<Appointment?> UpdateRemined(int id)
        {
            var treatment =await GetAppointmentById(id);
            if(treatment == null)
            {
                return null;
            }
            treatment.Isremaind ++;       
            return treatment;
        }

        public async Task<bool> UpdateAppointment(int id, Appointment appointment)
        {
            if(id != appointment.Idappointment)
            {
                return false;
            }
            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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

        public async Task<bool> UpdateRemark(int id, string remark)
        {
            var appointment = await GetAppointmentById(id);
            if(appointment == null)
            {
                return false;
            }
            appointment.Remark = remark;
            var isOk = await UpdateAppointment(id, appointment);
            return isOk;
        }

        public async Task<bool> UpdateIsPay(int id)
        {
            var appointment = await GetAppointmentById(id);
            if(appointment == null)
            {
                return false;
            }
            appointment.Ispay = true;
            var isOk = await UpdateAppointment(id, appointment);
            return isOk;
        }

        public async Task<List<Appointment>> GetAllPayedAppointments()
        {
            var appointment = await _context.Appointments.Where(a => a.Ispay == true).OrderBy(a => a.Date).ToListAsync();
            return appointment;
        }
    }
}







