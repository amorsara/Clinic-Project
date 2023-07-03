using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Appointments
{
    public interface IAppointmentsData
    {
        Task<List<Appointment>> GetAllAppointments();
        Task<Appointment?> GetAppointmentById(int id);
        Task<bool> CreateAppointment(Appointment appointments);
        bool AppointmentExists(int id);
    }
}
