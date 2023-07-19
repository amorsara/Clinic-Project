using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Epilations;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpilationareasController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly IEpilationData _iEpilationData;

        public EpilationareasController(ClinicDBContext context, IEpilationData epilationData)
        {
            _context = context;
            _iEpilationData = epilationData;
        }


        [HttpGet]
        [Route("/api/epilationareas/getallepilationareas")]
        public async Task<ActionResult<IEnumerable<Epilationarea>>> GetAllEpilationareas()
        {
            var epilationarea = await _iEpilationData.GetAllEpilationareas();
            if (epilationarea == null)
            {
                return NotFound();
            }
            return epilationarea;
        }

        [HttpGet]
        [Route("/api/epilationareas/getepilationareabyid/{id}")]
        public async Task<ActionResult<Epilationarea>> GetEpilationareaById(int id)
        {
            var epilationarea = await _iEpilationData.GetEpilationareaById(id);
            if (epilationarea == null)
            {
                return NotFound();
            }
            return epilationarea;
        }

        [HttpPut]
        [Route("/api/epilationareas/updateepilationarea/{id}")]
        public async Task<IActionResult> UpdateEpilationarea(int id, Epilationarea epilationarea)
        {
            if (id != epilationarea.Idepilationarea)
            {
                return BadRequest();
            }

            _context.Entry(epilationarea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_iEpilationData.EpilationareaExists(id))
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
        [Route("/api/epilationareas/createepilationareas")]
        public async Task<ActionResult<Epilationarea>> CreateEpilationarea(Epilationarea epilationarea)
        {
            var result = await _iEpilationData.CreateEpilationarea(epilationarea);
            if (result)
            {
                return CreatedAtAction("CreateEpilationarea", new { id = epilationarea.Idepilationarea }, epilationarea);
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpDelete]
        [Route("/api/epilationareas/deleteepilationarea/{id}")]
        public async Task<IActionResult> DeleteEpilationarea(int id)
        {
            if (_context.Epilationareas == null)
            {
                return NotFound();
            }
            var epilationarea = await _context.Epilationareas.FindAsync(id);
            if (epilationarea == null)
            {
                return NotFound();
            }

            _context.Epilationareas.Remove(epilationarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
