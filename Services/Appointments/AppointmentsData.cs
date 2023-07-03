using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
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

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<Appointment?> GetAppointmentById(int id)
        {
            var appointments = await _context.Appointments.FindAsync(id);
            return appointments;
        }
    }
}
