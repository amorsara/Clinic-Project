using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.EpilationTreatments;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpilationtreatmentsController : ControllerBase
    {
        private readonly IEpilationTreatmentData _iEpilationTreatmentData;

        public EpilationtreatmentsController(IEpilationTreatmentData epilationTreatmentData)
        {
            _iEpilationTreatmentData = epilationTreatmentData;
        }


        [HttpGet]
        [Route("/api/epilationtreatments/getepilationtreatments")]
        public async Task<ActionResult<IEnumerable<Epilationtreatment>>> GetEpilationtreatments()
        {
            var epilationtreatment = await _iEpilationTreatmentData.GetEpilationtreatments();
            if (epilationtreatment == null)
            {
                return NotFound();
            }
            return epilationtreatment;
        }

        [HttpGet]
        [Route("/api/epilationtreatments/getallepilationtreatments/{id}")]
        public async Task<ActionResult<EpilationCardDto>> GetAllEpilationtreatments(int id)
        {
            var epilationtreatments = await _iEpilationTreatmentData.GetAllEpilationTreatment(id);
            if (epilationtreatments == null)
            {
                return NotFound();
            }
            return epilationtreatments;
        }


        [HttpGet]
        [Route("/api/epilationtreatments/getepilationtreatmentbyid/{id}")]
        public async Task<ActionResult<Epilationtreatment>> GetEpilationtreatmentById(int id)
        {
            var epilationtreatment = await _iEpilationTreatmentData.GetEpilationtreatmentById(id);
            if (epilationtreatment == null)
            {
                return NotFound();
            }
            return epilationtreatment;
        }


        [HttpPut]
        [Route("/api/epilationtreatments/updateepilationtreatment/{id}")]
        public async Task<IActionResult> UpdateEpilationtreatment(int id, EpilationtreatmentDto epilationtreatmentDto)
        {
            if (id != epilationtreatmentDto.Idepilationtreatment)
            {
                return NoContent();
            }
            var epilationtreatment = await _iEpilationTreatmentData.GetEpilationtreatmentById(epilationtreatmentDto.Idepilationtreatment);
            if(epilationtreatment == null)
            {
                return NotFound();
            }
            epilationtreatment.Idcontact = epilationtreatmentDto.idClient;
            epilationtreatment.Date = epilationtreatmentDto.Date;
            epilationtreatment.Time = epilationtreatmentDto.Time;
            epilationtreatment.Coloremployee = epilationtreatmentDto.colorWorker;
            epilationtreatment.Machine = epilationtreatmentDto.Machine;
            epilationtreatment.Results = epilationtreatmentDto.Results;
            epilationtreatment.Techniqe = epilationtreatmentDto.Techniqe;
            epilationtreatment.Area = epilationtreatmentDto.Area?.Count != null ? String.Join(",", epilationtreatmentDto.Area) : null;
            if (epilationtreatment == null)
            {
                return NotFound();
            }

            var res = await _iEpilationTreatmentData.UpdateEpilationtreatment(id, epilationtreatment);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("/api/epilationtreatments/createepilationtreatment")]
        public async Task<ActionResult<Epilationtreatment>> CreateEpilationtreatment(EpilationtreatmentDto epilationtreatmentDto)
        {
            var epilationtreatment = new Epilationtreatment();
            epilationtreatment.Idcontact = epilationtreatmentDto.idClient;
            epilationtreatment.Date = epilationtreatmentDto.Date;
            epilationtreatment.Time = epilationtreatmentDto.Time;
            epilationtreatment.Coloremployee = epilationtreatmentDto.colorWorker;
            epilationtreatment.Machine = epilationtreatmentDto.Machine;
            epilationtreatment.Results = epilationtreatmentDto.Results;
            epilationtreatment.Techniqe = epilationtreatmentDto.Techniqe;
            epilationtreatment.Area = epilationtreatmentDto.Area?.Count != null ? String.Join(",", epilationtreatmentDto.Area) : null;
            var result = await _iEpilationTreatmentData.CreateEpilationtreatment(epilationtreatment);
            if (result)
            {
                return CreatedAtAction("CreateEpilationtreatment", new { id = epilationtreatment.Idepilationtreatment }, epilationtreatment);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("/api/epilationtreatments/deleteepilationtreatment/{id}")]
        public async Task<IActionResult> DeleteEpilationtreatment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var res = await _iEpilationTreatmentData.DeleteEpilationtreatmentById(id);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(res);
        }


    }
}
