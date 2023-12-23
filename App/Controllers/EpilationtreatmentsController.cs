using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Contacts;
using Services.DTO;
using Services.EpilationTreatments;
using Services.LaserTreatments;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpilationtreatmentsController : ControllerBase
    {
        private readonly IEpilationTreatmentData _iEpilationTreatmentData;
        private readonly IContactsData _iContactsData;

        public EpilationtreatmentsController(IEpilationTreatmentData epilationTreatmentData, IContactsData contactsData)
        {
            _iEpilationTreatmentData = epilationTreatmentData;
            _iContactsData = contactsData;
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

            if(epilationtreatmentDto.remarkElecr != null)
            {
                var okContact = await _iContactsData.UpdateRemark(epilationtreatmentDto.idClient, epilationtreatmentDto.remarkElecr, "epilation");
                if (okContact == false)
                {
                    return BadRequest();
                }
            }

            epilationtreatment.Idcontact = epilationtreatmentDto.idClient;
            epilationtreatment.Date = epilationtreatmentDto.Date;
            epilationtreatment.Coloremployee = epilationtreatmentDto.colorWorker;
            epilationtreatment.Results = epilationtreatmentDto.Results?.Count != null ? String.Join(",", epilationtreatmentDto.Results) : null; ;
            epilationtreatment.Probe = epilationtreatmentDto.Probe?.Count != null ? String.Join(",", epilationtreatmentDto.Probe) : null;
            epilationtreatment.Parameters = epilationtreatmentDto.Parameters?.Count != null ? String.Join(",", epilationtreatmentDto.Parameters) : null;
            epilationtreatment.Techniqe = epilationtreatmentDto.Techniqe?.Count != null ? String.Join(",", epilationtreatmentDto.Techniqe) : null;
            epilationtreatment.Area = epilationtreatmentDto.Area?.Count != null ? String.Join(",", epilationtreatmentDto.Area) : null;
            epilationtreatment.Machine = epilationtreatmentDto.Machine?.Count != null ? String.Join(",", epilationtreatmentDto.Machine) : null;
            epilationtreatment.Time = epilationtreatmentDto.Time?.Count != null ? String.Join(",", epilationtreatmentDto.Time) : null;

            var res = await _iEpilationTreatmentData.UpdateEpilationtreatment(id, epilationtreatment);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("/api/epilationtreatments/updatemedicallist/{id}")]
        public async Task<ActionResult> UpdateMedicalList(int id, List<MedicalListDto> medicalList)
        {
            var res = await _iContactsData.UpdateMedicalList(id, medicalList, "Epilation");
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("/api/epilationtreatments/createepilationtreatment")]
        public async Task<ActionResult<Epilationtreatment>> CreateEpilationtreatment(EpilationtreatmentDto epilationtreatmentDto)
        {

            var c = await _iContactsData.UpdateTreatementNameForContact(epilationtreatmentDto.idClient, "Electrolysis");
            if (c == null)
            {
                return BadRequest();
            }


            if (epilationtreatmentDto.remarkElecr != null)
            {
                var okContact = await _iContactsData.UpdateRemark(epilationtreatmentDto.idClient, epilationtreatmentDto.remarkElecr, "epilation");
                if (okContact == false)
                {
                    return BadRequest();
                }
            }

            var epilationtreatment = new Epilationtreatment();
            epilationtreatment.Idcontact = epilationtreatmentDto.idClient;
            epilationtreatment.Date = epilationtreatmentDto.Date;          
            epilationtreatment.Coloremployee = epilationtreatmentDto.colorWorker;           
            epilationtreatment.Results = epilationtreatmentDto.Results?.Count != null ? String.Join(",", epilationtreatmentDto.Results) : null; ;
            epilationtreatment.Probe = epilationtreatmentDto.Probe?.Count != null ? String.Join(",", epilationtreatmentDto.Probe) : null;
            epilationtreatment.Parameters = epilationtreatmentDto.Parameters?.Count != null ? String.Join(",", epilationtreatmentDto.Parameters) : null;
            epilationtreatment.Techniqe = epilationtreatmentDto.Techniqe?.Count != null ? String.Join(",", epilationtreatmentDto.Techniqe) : null;
            epilationtreatment.Area = epilationtreatmentDto.Area?.Count != null ? String.Join(",", epilationtreatmentDto.Area) : null;
            epilationtreatment.Machine = epilationtreatmentDto.Machine?.Count != null ? String.Join(",", epilationtreatmentDto.Machine) : null;
            epilationtreatment.Time = epilationtreatmentDto.Time?.Count != null ? String.Join(",", epilationtreatmentDto.Time) : null; 
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
