using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Waitings;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaitingsController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly IWaitingsData _iWaitingsData;

        public WaitingsController(ClinicDBContext context, IWaitingsData iWaitingsData)
        {
            _context = context;
            _iWaitingsData = iWaitingsData;
        }


        [HttpGet]
        [Route("/api/waitings/getallwaitings")]
        public async Task<ActionResult<IEnumerable<Waiting>>> GetWaitings()
        {
            var waiting = await _iWaitingsData.GetAllWaitings();
            if (waiting == null)
            {
                return NotFound();
            }
            return waiting;
        }

        [HttpGet]
        [Route("/api/waitings/getwaitingbyid/{id}")]
        public async Task<ActionResult<Waiting>> GetWaiting(int id)
        {
            var waiting = await _iWaitingsData.GetWaitingById(id);
            if (waiting == null)
            {
                return NotFound();
            }
            return waiting;
        }

        [HttpPut]
        [Route("/api/waitings/updatewaiting/{id}")]
        public async Task<IActionResult> UpdateWaiting(int id, Waiting waiting)
        {
            if (id != waiting.Idwaiting)
            {
                return BadRequest();
            }

            _context.Entry(waiting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_iWaitingsData.WaitingExists(id))
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
        [Route("/api/waitings/createwaiting")]
        public async Task<ActionResult<Waiting>> CreateWaiting(Waiting waiting)
        {
            var result = await _iWaitingsData.CreateWaiting(waiting);
            if (result)
            {
                return CreatedAtAction("CreateWaiting", new { id = waiting.Idwaiting }, waiting);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("/api/waitings/deletewaiting/{id}")]
        public async Task<IActionResult> DeleteWaiting(int id)
        {
            if (_context.Waitings == null)
            {
                return NotFound();
            }
            var waiting = await _context.Waitings.FindAsync(id);
            if (waiting == null)
            {
                return NotFound();
            }

            _context.Waitings.Remove(waiting);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
