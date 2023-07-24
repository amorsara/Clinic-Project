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
    public class TreatmentstypesController : ControllerBase
    {
        private readonly ClinicDBContext _context;

        public TreatmentstypesController(ClinicDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Treatmentstype>>> GetTreatmentstypes()
        {
          if (_context.Treatmentstypes == null)
          {
              return NotFound();
          }
            return await _context.Treatmentstypes.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Treatmentstype>> GetTreatmentstype(int id)
        {
          if (_context.Treatmentstypes == null)
          {
              return NotFound();
          }
            var treatmentstype = await _context.Treatmentstypes.FindAsync(id);

            if (treatmentstype == null)
            {
                return NotFound();
            }

            return treatmentstype;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTreatmentstype(int id, Treatmentstype treatmentstype)
        {
            if (id != treatmentstype.Idtreatmenttype)
            {
                return BadRequest();
            }

            _context.Entry(treatmentstype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreatmentstypeExists(id))
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
        public async Task<ActionResult<Treatmentstype>> CreateTreatmentstype(Treatmentstype treatmentstype)
        {
          if (_context.Treatmentstypes == null)
          {
              return Problem("Entity set 'ClinicDBContext.Treatmentstypes'  is null.");
          }
            _context.Treatmentstypes.Add(treatmentstype);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateTreatmentstype", new { id = treatmentstype.Idtreatmenttype }, treatmentstype);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreatmentstype(int id)
        {
            if (_context.Treatmentstypes == null)
            {
                return NotFound();
            }
            var treatmentstype = await _context.Treatmentstypes.FindAsync(id);
            if (treatmentstype == null)
            {
                return NotFound();
            }

            _context.Treatmentstypes.Remove(treatmentstype);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TreatmentstypeExists(int id)
        {
            return (_context.Treatmentstypes?.Any(e => e.Idtreatmenttype == id)).GetValueOrDefault();
        }
    }
}
