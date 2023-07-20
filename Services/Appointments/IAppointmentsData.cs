using Microsoft.AspNetCore.Mvc;
using Repository.GeneratedModels;
using Services.DTO;
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
        Task<List<Appointment>> GetWaitAppointments();
        Task<List<Appointment>> GetAllWaitDates();
        Task<List<DateOnly>> GetDatesOfAppointments(int id);
        Task<List<DateOnly>> GetAllFutureDatesById(int id);
        Task<Appointment?> GetAppointmentById(int id);
        Task<bool> CreateAppointment(Appointment appointments);
        bool AppointmentExists(int id);
        Task<Appointment?> UpdateRemined(int id);
        Task<Appointment?> DeleteWait(int id);
    }
}
