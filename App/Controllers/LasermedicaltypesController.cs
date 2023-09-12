using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.LaserMedicalTypes;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LasermedicaltypesController : ControllerBase
    {
        private readonly ILaserMedicalTypesData _iLaserMedicalTypesData;

        public LasermedicaltypesController(ILaserMedicalTypesData laserMedicalTypesData)
        {
            _iLaserMedicalTypesData = laserMedicalTypesData;
        }


        [HttpGet]
        [Route("/api/lasermedicaltypes/getalllasermedicaltypes")]
        public async Task<ActionResult<IEnumerable<Lasermedicaltype>>> GetAllLasermedicaltypes()
        {
            var lasermedicaltypes = await _iLaserMedicalTypesData.GetAllLasermedicaltypes();
            if (lasermedicaltypes == null)
            {
                return NotFound();
            }
            return lasermedicaltypes;
        }


        [HttpGet]
        [Route("/api/lasermedicaltypes/getlasermedicaltypebyid/{id}")]
        public async Task<ActionResult<Lasermedicaltype>> GetLasermedicaltypeById(int id)
        {
            var lasermedicaltype = await _iLaserMedicalTypesData.GetLasermedicaltypeById(id);
            if (lasermedicaltype == null)
            {
                return NotFound();
            }
            return lasermedicaltype;
        }

        [HttpPut]
        [Route("/api/lasermedicaltypes/updatelasermedicaltype/{id}")]
        public async Task<IActionResult> UpdateLasermedicaltype(int id, Lasermedicaltype lasermedicaltype)
        {
            if (id != lasermedicaltype.Idlasermedicaltype)
            {
                return NoContent();
            }
            var res = await _iLaserMedicalTypesData.UpdateLasermedicaltype(id, lasermedicaltype);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("/api/lasermedicaltypes/createlasermedicaltype")]
        public async Task<ActionResult<Lasermedicaltype>> CreateLasermedicaltype(Lasermedicaltype lasermedicaltype)
        {
            var result = await _iLaserMedicalTypesData.CreateLasermedicaltype(lasermedicaltype);
            if (result)
            {
                return CreatedAtAction("CreateLasermedicaltype", new { id = lasermedicaltype.Idlasermedicaltype }, lasermedicaltype);
            }
            else
            {
                return BadRequest();
            }
           
        }

        [HttpDelete]
        [Route("/api/lasermedicaltypes/deletelasermedicaltype/{id}")]
        public async Task<IActionResult> DeleteLasermedicaltype(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var res = await _iLaserMedicalTypesData.DeleteLasermedicaltype(id);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(res);
        }
    }
}
