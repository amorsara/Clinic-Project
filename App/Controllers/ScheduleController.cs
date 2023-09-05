using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        [Route("/api/schedule/getalldatesforweek")]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetAllDatesForWeek(DateOnly date)
        {
            var schedules = await _iScheduleData.GetAllDates(date);
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

        //[HttpPost]
        //[Route("/api/schedule/getallschedulesforweek")]
        //public async Task<ActionResult<List<RoomScheduleDto>>> GetAllSchedulesForWeek(DateOnly date)
        //{
        //    var schedules = await _iScheduleData.GetAllSchedulesForWeek(date);
        //    if (schedules == null)
        //    {
        //        return NotFound();
        //    }
        //    return schedules;
        //}
    }
}
