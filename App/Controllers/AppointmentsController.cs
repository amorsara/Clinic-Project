using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Appointments;
using Services.Contacts;
using Services.DTO;
using Services.Employees;
using Services.Schedule;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly IAppointmentsData _iAppointmentsData;
        private readonly IEmployeesData _iIEmployeesData;
        private readonly IContactsData _iContactsData;

        public AppointmentsController(ClinicDBContext context, IAppointmentsData appointmentsData, IEmployeesData employeesData, IContactsData contactsData)
        {
            _context = context;
            _iAppointmentsData = appointmentsData;
            _iIEmployeesData = employeesData;
            _iContactsData = contactsData;
        }

        [HttpGet]
        [Route("/api/appointments/getallappointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
        {
            var appointments = await _iAppointmentsData.GetAllAppointments();
            if (appointments == null)
            {
                return NotFound();
            }
            return appointments;
        }

        [HttpGet]
        [Route("/api/appointments/getappointmentbyid/{id}")]
        public async Task<ActionResult<Appointment>> GetAppointmentById(int id)
        {
            var appointment = await _iAppointmentsData.GetAppointmentById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return appointment;
        }

        [HttpGet]
        [Route("/api/appointments/updateremined/{id}")]
        public async Task<ActionResult<Appointment>> UpdateRemined(int id)
        {
            var appointment = await _iAppointmentsData.UpdateRemined(id);
            if (appointment == null)
            {
                return NotFound();
            }
            await UpdateAppointment(id, appointment);
            return Ok(appointment);
        }

        [HttpGet]
        [Route("/api/appointments/updateremark/{id}/{remark}")]
        public async Task<ActionResult<Appointment>> UpdateRemark(int id, string remark)
        {
            var res = await _iAppointmentsData.UpdateRemark(id, remark);
            if (res == false)
            {
                return BadRequest();
            }
         
            return Ok(res);
        }

        [HttpPut]
        [Route("/api/appointments/updateappointment/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Idappointment)
            {
                return NoContent();
            }

            var res = await _iAppointmentsData.UpdateAppointment(id, appointment);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }
      
        [HttpPost]
        [Route("/api/appointments/createappointment")]
        public async Task<ActionResult<Appointment>> CreateAppointmentWarraper(AppointmentDto appointment)
        {
            var newAppointment = new Appointment();
            newAppointment.Discount = appointment.discount;
            newAppointment.Date = appointment.Date;
            newAppointment.Idroom = appointment.idRoom;
            newAppointment.Idcontact = appointment.idTreated;
            newAppointment.Treatmentname = appointment.treatment;
            newAppointment.Remark = appointment.Remark;
            newAppointment.Isremaind = appointment.isRemined == true ? 1:0;
            newAppointment.Timestart = appointment.startHouer;
            newAppointment.Timeend = appointment.endTime;
            newAppointment.Duration = appointment.duration;
            newAppointment.Area = appointment.area?.Count != null ? String.Join(", ", appointment.area): null;
            newAppointment.Idemployee = appointment.idWorker;
            newAppointment.Cancle = false;
            var res = await CreateAppointment(newAppointment);
            var c = await _iContactsData.UpdateTreatementNameForContact(appointment.idTreated, appointment.treatment);
            return res;
        }

        [HttpPost]
        [Route("/api/appointments/createappointment1")]
        public async Task<ActionResult<Appointment>> CreateAppointment(Appointment appointment)
        {
            var result = await _iAppointmentsData.CreateAppointment(appointment);
            if (result)
            {
                return CreatedAtAction("CreateAppointment", new { id = appointment.Idappointment }, appointment);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("/api/appointments/deleteappointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (_context.Appointments == null)
            {
                return NotFound();
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
