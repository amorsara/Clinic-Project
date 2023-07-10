using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Appointments;
using Services.DTO;
using Services.Schedule;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly IAppointmentsData _iAppointmentsData;

        public AppointmentsController(ClinicDBContext context, IAppointmentsData appointmentsData)
        {
            _context = context;
            _iAppointmentsData = appointmentsData;
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
        [Route("/api/appointments/getwaitappointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetWaitAppointments()
        {
            var appointments = await _iAppointmentsData.GetWaitAppointments();
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
        [Route("/api/appointments/deletewait/{id}")]
        public async Task<ActionResult<Appointment>> DeleteWait(int id)
        {
            var appointment = await _iAppointmentsData.DeleteWait(id);
            if (appointment == null)
            {
                return NotFound();
            }
            await UpdateAppointment(id, appointment);
            return Ok(appointment);
        }
       
        [HttpPut]
        [Route("/api/appointments/updateappointment/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Idappointment)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_iAppointmentsData.AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
      
        [HttpPost]
        [Route("/api/appointments/createappointment")]
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
