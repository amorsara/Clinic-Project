﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.LaserTreatments;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LasertreatmentsController : ControllerBase
    {

        private readonly ILaserTreatmentData _iLaserTreatmentData;

        public LasertreatmentsController(ILaserTreatmentData iLaserTreatmentData)
        {
            _iLaserTreatmentData = iLaserTreatmentData;
        }

        [HttpGet]
        [Route("/api/lasertreatments/getlasertreatments")]
        public async Task<ActionResult<IEnumerable<Lasertreatment>>> GetLasertreatments()
        {
            var lasertreatments = await _iLaserTreatmentData.GetLasertreatments();
            if (lasertreatments == null)
            {
                return NotFound();
            }
            return lasertreatments;
        }

        [HttpGet]
        [Route("/api/lasertreatments/getalllasertreatments/{id}")]
        public async Task<ActionResult<LaserCardDto>> GetAllLasertreatments(int id)
        {
            var lasertreatments = await _iLaserTreatmentData.GetAllLaserTreatment(id);
            if (lasertreatments == null)
            {
                return NotFound();
            }
            return lasertreatments;
        }


        [HttpGet]
        [Route("/api/lasertreatments/getlasertreatmentsbyid/{id}")]
        public async Task<ActionResult<Lasertreatment>> GetLasertreatmentById(int id)
        {
            var lasertreatment = await _iLaserTreatmentData.GetLasertreatmentById(id);
            if (lasertreatment == null)
            {
                return NotFound();
            }
            return lasertreatment;
        }

        [HttpPut]
        [Route("/api/lasertreatments/updatelasertreatment/{id}")]
        public async Task<IActionResult> UpdateLasertreatment(int id, LasertreatmentDto lasertreatmentDto)
        {
            if (id != lasertreatmentDto.Idlasertreatment)
            {
                return NoContent();
            }

            var lasertreatment = await _iLaserTreatmentData.GetLasertreatmentById(lasertreatmentDto.Idlasertreatment);
            if (lasertreatment == null)
            {
                return NotFound();
            }

            //lasertreatment.Idlasertreatment = lasertreatmentDto.Idlasertreatment;
            lasertreatment.Idcontact = lasertreatmentDto.idClient;
            lasertreatment.Date = lasertreatmentDto.Date;
            lasertreatment.Area = lasertreatmentDto.Area?.Count != null ? String.Join(",", lasertreatmentDto.Area) : null;
            lasertreatment.Coloremployee = lasertreatmentDto.colorWorker;
            lasertreatment.Results = lasertreatmentDto.Results;
            lasertreatment.Energy = lasertreatmentDto.Energy;
            lasertreatment.Spotsize = lasertreatmentDto.Spotsize;
            lasertreatment.Ms = lasertreatmentDto.Ms;

            var res = await _iLaserTreatmentData.UpdateLasertreatment(id, lasertreatment);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("/api/lasertreatments/createlasertreatment")]
        public async Task<ActionResult<Lasertreatment>> CreateLasertreatment(LasertreatmentDto lasertreatmentDto)
        {
            var lasertreatment = new Lasertreatment();
            lasertreatment.Idlasertreatment = lasertreatmentDto.Idlasertreatment;
            lasertreatment.Idcontact = lasertreatmentDto.idClient;
            lasertreatment.Date = lasertreatmentDto.Date;
            lasertreatment.Area = lasertreatmentDto.Area?.Count != null ? String.Join(",", lasertreatmentDto.Area) : null;
            lasertreatment.Coloremployee = lasertreatmentDto.colorWorker;
            lasertreatment.Results = lasertreatmentDto.Results;
            lasertreatment.Energy = lasertreatmentDto.Energy;
            lasertreatment.Spotsize = lasertreatmentDto.Spotsize;
            lasertreatment.Ms = lasertreatmentDto.Ms;
            var result = await _iLaserTreatmentData.CreateLasertreatments(lasertreatment);
            if (result)
            {
                return CreatedAtAction("CreateLasertreatment", new { id = lasertreatmentDto.Idlasertreatment }, lasertreatmentDto);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("/api/lasertreatments/deletelasertreatment/{id}")]
        public async Task<IActionResult> DeleteLasertreatment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var res = await _iLaserTreatmentData.DeleteLasertreatmentById(id);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(res);
        }
    }
}