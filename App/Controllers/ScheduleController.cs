using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using Quartz.Impl;
using Services.CloseEvents;
using Services.DTO;
using Services.Schedule;
using System.Threading;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {

        private readonly IScheduleData _iScheduleData;
        private readonly ICloseEvents _iCloseEvents;

        public ScheduleController(IScheduleData scheduleData, ICloseEvents closeEvents)
        {
            _iScheduleData = scheduleData;
            _iCloseEvents = closeEvents;
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

        [HttpPost]
        [Route("/api/schedule/getalldatesforweek")]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetAllDatesForWeek(DateDto date)
        {
            var schedules = await _iScheduleData.GetAllDates(date.sunday);
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
            var schedules = await _iScheduleData.GetAllSchedules(false);
            if (schedules == null)
            {
                return NotFound();
            }
            return schedules;
        }

        [HttpGet]
        [Route("/api/schedule/getscheduleextra")]
        public async Task<ActionResult<IEnumerable<RoomScheduleDto>>> GetScheduleExtra()
        {
            var schedules = await _iScheduleData.GetAllSchedules(true);
            if (schedules == null)
            {
                return NotFound();
            }
            return schedules;
        }

        [HttpPost]
        [Route("/api/schedule/getscheduleforcloseevents")]
        public async Task<ActionResult<IEnumerable<CloseEventsDto>>> GetScheduleForCloseEventsForWeek(DateDto date)
        {
            var schedules = await _iCloseEvents.GetAllCloseEventsForWeek(date.sunday);
            if (schedules == null)
            {
                return NotFound();
            }
            return schedules;
        }
    }
}
