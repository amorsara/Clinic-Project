using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Lesers;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeserareasController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly ILeserData _iLeserData;

        public LeserareasController(ClinicDBContext context, ILeserData leserData)
        {
            _context = context;
            _iLeserData = leserData;
        }

        [HttpGet]
        [Route("/api/leserareas/getallleserareas")]
        public async Task<ActionResult<IEnumerable<Leserarea>>> GetAllLeserareas()
        {
            var leserarea = await _iLeserData.GetAllLeserareas();
            if (leserarea == null)
            {
                return NotFound();
            }
            return leserarea;
        }


        [HttpGet]
        [Route("/api/leserareas/getleserareabyid/{id}")]
        public async Task<ActionResult<Leserarea>> GetLeserareaById(int id)
        {
            var leserarea = await _iLeserData.GetLeserareaById(id);
            if (leserarea == null)
            {
                return NotFound();
            }
            return leserarea;
        }

        [HttpPut]
        [Route("/api/leserareas/updateleserarea/{id}")]
        public async Task<IActionResult> UpdateLeserarea(int id, Leserarea leserarea)
        {
            if (id != leserarea.Idleserarea)
            {
                return BadRequest();
            }

            _context.Entry(leserarea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_iLeserData.LeserareaExists(id))
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
        [Route("/api/leserareas/createleserarea")]
        public async Task<ActionResult<Leserarea>> CreateLeserarea(Leserarea leserarea)
        {
            var result = await _iLeserData.CreateLeserarea(leserarea);
            if (result)
            {
                return CreatedAtAction("CreateLeserarea", new { id = leserarea.Idleserarea }, leserarea);
            }
            else
            {
                return BadRequest();
            }
           
        }

        [HttpDelete]
        [Route("/api/leserareas/deleteleserarea/{id}")]
        public async Task<IActionResult> DeleteLeserarea(int id)
        {
            if (_context.Leserareas == null)
            {
                return NotFound();
            }
            var leserarea = await _context.Leserareas.FindAsync(id);
            if (leserarea == null)
            {
                return NotFound();
            }

            _context.Leserareas.Remove(leserarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
