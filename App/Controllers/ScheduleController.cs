﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Schedule;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {

        private readonly IScheduleData _iScheduleData;

        public ScheduleController(IScheduleData scheduleData)
        {
            _iScheduleData = scheduleData;
        }

        [HttpGet]
        [Route("/api/schedule/getalldates")]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetAllDates()
        {
            var schedules = await _iScheduleData.GetAllDates();
            if (schedules == null)
            {
                return NotFound();
            }
            return schedules;
        }

        [HttpGet]
        [Route("/api/schedule/getschedule")]
        public async Task<ActionResult<IEnumerable<RoomScheduleDto>>> GetSchedule()
        {
            var schedules = await _iScheduleData.GetAllSchedules();
            if (schedules == null)
            {
                return NotFound();
            }
            return schedules;
        }

    }
}