using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.WorkHours;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkhoursController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly IWorkHoursData _iWorkHoursData;

        public WorkhoursController(ClinicDBContext context, IWorkHoursData workHoursData)
        {
            _context = context;
            _iWorkHoursData = workHoursData;
        }

        [HttpGet]
        [Route("/api/workhours/getallworkhour")]
        public async Task<ActionResult<IEnumerable<Workhour>>> GetAllWorkhours()
        {
            var workhours = await _iWorkHoursData.GetAllWorkHours();
            if (workhours == null)
            {
                return NotFound();
            }
            return workhours;
        }

        [HttpGet]
        [Route("/api/workhours/getworkhourbyid/{id}")]
        public async Task<ActionResult<Workhour>> GetWorkhourById(int id)
        {
            var workhour = await _iWorkHoursData.GetWorkHourById(id);
            if (workhour == null)
            {
                return NotFound();
            }
            return workhour;
        }

        //[HttpGet]
        //[Route("/api/workhours/getworkhourbyemployee/{id}")]
        //public async Task<ActionResult<IEnumerable<Workhour>>> GetWorkhourByEmployee(int id)
        //{
        //    var listHours = await _iWorkHoursData.GetWorkHourByEmployee(id);
        //    if(listHours == null)
        //    {
        //        return NotFound();
        //    }
        //    return listHours;
        //}

        [HttpPut]
        [Route("/api/workhours/updateworkhour/{id}")]
        public async Task<IActionResult> UpdateWorkhour(int id, Workhour workhour)
        {
            if (id != workhour.Idworkhour)
            {
                return BadRequest();
            }

            _context.Entry(workhour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_iWorkHoursData.WorkHourExists(id))
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
        [Route("/api/workhours/createworkhour")]
        public async Task<ActionResult<Workhour>> CreateWorkhour(Workhour workhour)
        {
            var result = await _iWorkHoursData.CreateWorkHour(workhour);
            if (result)
            {
                return CreatedAtAction("CreateWorkhour", new { id = workhour.Idworkhour }, workhour);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("/api/workhours/deleteworkhour/{id}")]
        public async Task<IActionResult> DeleteWorkhour(int id)
        {
            if (_context.Workhours == null)
            {
                return NotFound();
            }
            var workhour = await _context.Workhours.FindAsync(id);
            if (workhour == null)
            {
                return NotFound();
            }

            _context.Workhours.Remove(workhour);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
