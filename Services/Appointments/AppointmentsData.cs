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

        public async Task<bool> CreateAppointment(Appointment appointments)
        {
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

        public async Task<Appointment?> DeleteWait(int id)
        {
            var appointment = await GetAppointmentById(id);
            if(appointment == null)
            {
                return null;
            }
            appointment.Wait = false;
            return appointment;
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await _context.Appointments.ToListAsync();
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

        public async Task<List<DateOnly>> GetDatesOfAppointments(int id)
        {
            var appointments = await _context.Appointments.Where(a => a.Idcontact == id).ToListAsync();
            var listDate = new List<DateOnly>();
            foreach(var appointment in appointments)
            {
                if(appointment.Date != null)
                {
                    listDate.Add((DateOnly)appointment.Date);

                }
            }
            return listDate;
        }

        public async Task<List<Appointment>> GetWaitAppointments()
        {
            return await _context.Appointments.Where(a => a.Wait == true).ToListAsync();
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
    }
}
