using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.TreatmentsType;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentstypesController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly ITreatmentsTypeData _iTreatmentsTypeData;

        public TreatmentstypesController(ClinicDBContext context, ITreatmentsTypeData treatmentsTypeData)
        {
            _context = context;
            _iTreatmentsTypeData = treatmentsTypeData;
        }

        [HttpGet]
        [Route("/api/treatmentstypes/gettretmentstype")]
        public async Task<ActionResult<IEnumerable<Treatmentstype>>> GetAllTreatmentstypes()
        {
            var treatmentstypes = await _iTreatmentsTypeData.GetAllTreatmentstypes();
            if (treatmentstypes == null)
            {
                return NotFound();
            }
            return treatmentstypes;
        }

        [HttpGet]
        [Route("/api/treatmentstypes/getalltretmentstype")]
        public async Task<ActionResult<IEnumerable<string>>> GetlistTreatmentstypes()
        {
            var treatmentstypes = await _iTreatmentsTypeData.GetlistTreatmentstypes();
            if(treatmentstypes == null)
            {
                return NotFound();
            }
            return treatmentstypes;
        }


        [HttpGet]
        [Route("/api/treatmentstypes/gettreatmentstypebyid/{id}")]
        public async Task<ActionResult<Treatmentstype>> GetTreatmentstypeById(int id)
        {
            var treatmentstype = await _iTreatmentsTypeData.GetTreatmentstypeById(id);
            if (treatmentstype == null)
            {
                return NotFound();
            }
            return treatmentstype;
        }

        [HttpPut]
        [Route("/api/treatmentstypes/updatettreatmentstype/{id}")]
        public async Task<IActionResult> UpdatetTreatmentstype(int id, Treatmentstype treatmentstype)
        {    
            if (id != treatmentstype.Idtreatmenttype)
            {
                return BadRequest();
            }

            var res = await _iTreatmentsTypeData.UpdatetTreatmentstype(id, treatmentstype);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("/api/treatmentstypes/createtreatmentstype")]
        public async Task<ActionResult<Treatmentstype>> CreateTreatmentstype(Treatmentstype treatmentstype)
        {
            var result = await _iTreatmentsTypeData.CreateTreatmentstype(treatmentstype);
            if (result)
            {
                return CreatedAtAction("CreateTreatmentstype", new { id = treatmentstype.Idtreatmenttype }, treatmentstype);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("/api/treatmentstypes/deletetreatmentstype/{id}")]
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


    }
}
