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

        Task<List<Appointment>> GetAllAppointmentsForWeek(DateOnly? date = null);

        Task<List<Appointment>> GetWaitAppointments();

        Task<List<Appointment>> GetAllWaitDates();

        Task<List<FutureDateDto>> GetDatesOfAppointments(int id);

        Task<List<DateOnly>> GetAllFutureDatesById(int id);

        Task<Appointment?> GetAppointmentById(int id);

        Task<bool> CancelAppointment(int idRoom, int idEmployee, DateOnly? sDate, DateOnly? eDate, TimeOnly? sTime, TimeOnly? eTime);

        Task<bool> CreateAppointment(Appointment appointments);

        bool AppointmentExists(int id);

        Task<Appointment?> UpdateRemined(int id);

        Task<bool> UpdateAppointment(int id, Appointment appointment);

        Task<Appointment?> DeleteWait(int id);
    }
}
