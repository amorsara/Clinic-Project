using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Attendances;
using Services.DTO;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendancesData _iAttendancesData;

        public AttendancesController(IAttendancesData attendancesData)
        {
            _iAttendancesData = attendancesData;
        }

        [HttpGet]
        [Route("/api/attendances/getattendances")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendances()
        {
            var attendances = await _iAttendancesData.GetAttendances();
            if (attendances == null)
            {
                return NotFound();
            }
            return attendances;
        }

        [HttpGet]
        [Route("/api/attendances/getallattendances")]
        public async Task<ActionResult<IEnumerable<AllAttendanceDto>>> GetAllAttendances()
        {
            var attendances = await _iAttendancesData.GetAllAttendances();
            if (attendances == null)
            {
                return NotFound();
            }
            return attendances;
        }

        [HttpGet]
        [Route("/api/attendances/getattendancebyid/{id}")]
        public async Task<ActionResult<Attendance>> GetAttendanceById(int id)
        {
            var attendance = await _iAttendancesData.GetAttendanceById(id);
            if (attendance == null)
            {
                return NotFound();
            }
            return attendance;
        }


        [HttpPut]
        [Route("/api/attendances/updateattendance/{id}")]
        public async Task<IActionResult> UpdateAttendance(int id, Attendance attendance)
        {
            if (id != attendance.Idattendance)
            {
                return NoContent();
            }

            var res = await _iAttendancesData.UpdateAttendance(id, attendance);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("/api/attendances/enterattendance")]
        public async Task<ActionResult> EnterAttendance(AttendanceDto attendancedto)
        {
            if(attendancedto == null)
            {
                return NoContent();
            }
            var attendance = new Attendance();
            attendance.Idemployee = attendancedto.employee?.Id == null ? 0 : attendancedto.employee.Id;
            attendance.Date = attendancedto.date;
            attendance.Timeenter = attendancedto.time;

            var res = await _iAttendancesData.CreateAttendance(attendance);
            if(res == false)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("/api/attendances/exitattendance")]
        public async Task<ActionResult> ExitAttendance(AttendanceDto attendancedto)
        {
            if (attendancedto == null)
            {
                return NoContent();
            }
            var attendance = new Attendance();
            attendance.Idemployee = attendancedto.employee?.Id == null ? 0 : attendancedto.employee.Id;
            attendance.Date = attendancedto.date;
            attendance.Timeexit = attendancedto.time;

            var res = await _iAttendancesData.ExitAttendance(attendance);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(res);
        }


        [HttpPost]
        [Route("/api/attendances/createattendance")]
        public async Task<ActionResult<Attendance>> CreateAttendance(Attendance attendance)
        {
            if(attendance == null)
            {
                return BadRequest();
            }
          
            var result = await _iAttendancesData.CreateAttendance(attendance);
            if (result)
            {
                return CreatedAtAction("CreateAttendance", new { id = attendance.Idattendance }, attendance);
            }
            else
            {
                return BadRequest();
            }
            
        }


        [HttpDelete]
        [Route("/api/attendances/deleteattendance/{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var res = await _iAttendancesData.DeleteAttendance(id);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(res);
        }
    }
}
