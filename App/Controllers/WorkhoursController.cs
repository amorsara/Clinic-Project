using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkhoursController : ControllerBase
    {
        private readonly ClinicDBContext _context;

        public WorkhoursController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: api/Workhours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workhour>>> GetWorkhours()
        {
          if (_context.Workhours == null)
          {
              return NotFound();
          }
            return await _context.Workhours.ToListAsync();
        }

        // GET: api/Workhours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Workhour>> GetWorkhour(int id)
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

            return workhour;
        }

        // PUT: api/Workhours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkhour(int id, Workhour workhour)
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
                if (!WorkhourExists(id))
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

        // POST: api/Workhours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Workhour>> PostWorkhour(Workhour workhour)
        {
          if (_context.Workhours == null)
          {
              return Problem("Entity set 'ClinicDBContext.Workhours'  is null.");
          }
            _context.Workhours.Add(workhour);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkhour", new { id = workhour.Idworkhour }, workhour);
        }

        // DELETE: api/Workhours/5
        [HttpDelete("{id}")]
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

        private bool WorkhourExists(int id)
        {
            return (_context.Workhours?.Any(e => e.Idworkhour == id)).GetValueOrDefault();
        }
    }
}
