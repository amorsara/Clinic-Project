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
    public class EpilationmedicaltypesController : ControllerBase
    {
        private readonly ClinicDBContext _context;

        public EpilationmedicaltypesController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: api/Epilationmedicaltypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Epilationmedicaltype>>> GetEpilationmedicaltypes()
        {
          if (_context.Epilationmedicaltypes == null)
          {
              return NotFound();
          }
            return await _context.Epilationmedicaltypes.ToListAsync();
        }

        // GET: api/Epilationmedicaltypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Epilationmedicaltype>> GetEpilationmedicaltype(int id)
        {
          if (_context.Epilationmedicaltypes == null)
          {
              return NotFound();
          }
            var epilationmedicaltype = await _context.Epilationmedicaltypes.FindAsync(id);

            if (epilationmedicaltype == null)
            {
                return NotFound();
            }

            return epilationmedicaltype;
        }

        // PUT: api/Epilationmedicaltypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEpilationmedicaltype(int id, Epilationmedicaltype epilationmedicaltype)
        {
            if (id != epilationmedicaltype.Idepilationmedicaltype)
            {
                return BadRequest();
            }

            _context.Entry(epilationmedicaltype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpilationmedicaltypeExists(id))
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

        // POST: api/Epilationmedicaltypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Epilationmedicaltype>> PostEpilationmedicaltype(Epilationmedicaltype epilationmedicaltype)
        {
          if (_context.Epilationmedicaltypes == null)
          {
              return Problem("Entity set 'ClinicDBContext.Epilationmedicaltypes'  is null.");
          }
            _context.Epilationmedicaltypes.Add(epilationmedicaltype);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEpilationmedicaltype", new { id = epilationmedicaltype.Idepilationmedicaltype }, epilationmedicaltype);
        }

        // DELETE: api/Epilationmedicaltypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpilationmedicaltype(int id)
        {
            if (_context.Epilationmedicaltypes == null)
            {
                return NotFound();
            }
            var epilationmedicaltype = await _context.Epilationmedicaltypes.FindAsync(id);
            if (epilationmedicaltype == null)
            {
                return NotFound();
            }

            _context.Epilationmedicaltypes.Remove(epilationmedicaltype);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EpilationmedicaltypeExists(int id)
        {
            return (_context.Epilationmedicaltypes?.Any(e => e.Idepilationmedicaltype == id)).GetValueOrDefault();
        }
    }
}
