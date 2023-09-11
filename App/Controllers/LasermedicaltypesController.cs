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
    public class LasermedicaltypesController : ControllerBase
    {
        private readonly ClinicDBContext _context;

        public LasermedicaltypesController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: api/Lasermedicaltypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lasermedicaltype>>> GetLasermedicaltypes()
        {
          if (_context.Lasermedicaltypes == null)
          {
              return NotFound();
          }
            return await _context.Lasermedicaltypes.ToListAsync();
        }

        // GET: api/Lasermedicaltypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lasermedicaltype>> GetLasermedicaltype(int id)
        {
          if (_context.Lasermedicaltypes == null)
          {
              return NotFound();
          }
            var lasermedicaltype = await _context.Lasermedicaltypes.FindAsync(id);

            if (lasermedicaltype == null)
            {
                return NotFound();
            }

            return lasermedicaltype;
        }

        // PUT: api/Lasermedicaltypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLasermedicaltype(int id, Lasermedicaltype lasermedicaltype)
        {
            if (id != lasermedicaltype.Idlasermedicaltype)
            {
                return BadRequest();
            }

            _context.Entry(lasermedicaltype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LasermedicaltypeExists(id))
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

        // POST: api/Lasermedicaltypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lasermedicaltype>> PostLasermedicaltype(Lasermedicaltype lasermedicaltype)
        {
          if (_context.Lasermedicaltypes == null)
          {
              return Problem("Entity set 'ClinicDBContext.Lasermedicaltypes'  is null.");
          }
            _context.Lasermedicaltypes.Add(lasermedicaltype);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLasermedicaltype", new { id = lasermedicaltype.Idlasermedicaltype }, lasermedicaltype);
        }

        // DELETE: api/Lasermedicaltypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLasermedicaltype(int id)
        {
            if (_context.Lasermedicaltypes == null)
            {
                return NotFound();
            }
            var lasermedicaltype = await _context.Lasermedicaltypes.FindAsync(id);
            if (lasermedicaltype == null)
            {
                return NotFound();
            }

            _context.Lasermedicaltypes.Remove(lasermedicaltype);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LasermedicaltypeExists(int id)
        {
            return (_context.Lasermedicaltypes?.Any(e => e.Idlasermedicaltype == id)).GetValueOrDefault();
        }
    }
}
