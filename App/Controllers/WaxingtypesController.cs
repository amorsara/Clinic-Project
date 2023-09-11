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
    public class WaxingtypesController : ControllerBase
    {
        private readonly ClinicDBContext _context;

        public WaxingtypesController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: api/Waxingtypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Waxingtype>>> GetWaxingtypes()
        {
          if (_context.Waxingtypes == null)
          {
              return NotFound();
          }
            return await _context.Waxingtypes.ToListAsync();
        }

        // GET: api/Waxingtypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Waxingtype>> GetWaxingtype(int id)
        {
          if (_context.Waxingtypes == null)
          {
              return NotFound();
          }
            var waxingtype = await _context.Waxingtypes.FindAsync(id);

            if (waxingtype == null)
            {
                return NotFound();
            }

            return waxingtype;
        }

        // PUT: api/Waxingtypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWaxingtype(int id, Waxingtype waxingtype)
        {
            if (id != waxingtype.Idwaxingtype)
            {
                return BadRequest();
            }

            _context.Entry(waxingtype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaxingtypeExists(id))
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

        // POST: api/Waxingtypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Waxingtype>> PostWaxingtype(Waxingtype waxingtype)
        {
          if (_context.Waxingtypes == null)
          {
              return Problem("Entity set 'ClinicDBContext.Waxingtypes'  is null.");
          }
            _context.Waxingtypes.Add(waxingtype);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWaxingtype", new { id = waxingtype.Idwaxingtype }, waxingtype);
        }

        // DELETE: api/Waxingtypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWaxingtype(int id)
        {
            if (_context.Waxingtypes == null)
            {
                return NotFound();
            }
            var waxingtype = await _context.Waxingtypes.FindAsync(id);
            if (waxingtype == null)
            {
                return NotFound();
            }

            _context.Waxingtypes.Remove(waxingtype);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WaxingtypeExists(int id)
        {
            return (_context.Waxingtypes?.Any(e => e.Idwaxingtype == id)).GetValueOrDefault();
        }
    }
}
