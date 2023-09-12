using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.EpilationMedicalTypes;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpilationmedicaltypesController : ControllerBase
    {
        private readonly IEpilationMedicalTypesData _iEpilationMedicalTypesData;

        public EpilationmedicaltypesController(IEpilationMedicalTypesData epilationMedicalTypesData)
        {
           _iEpilationMedicalTypesData = epilationMedicalTypesData;
        }

        [HttpGet]
        [Route("/api/epilationmedicaltypes/getallepilationmedicaltypes")]
        public async Task<ActionResult<IEnumerable<Epilationmedicaltype>>> GetAllEpilationmedicaltypes()
        {
            var epilationmedicaltypes = await _iEpilationMedicalTypesData.GetAllEpilationmedicaltypes();
            if (epilationmedicaltypes == null)
            {
                return NotFound();
            }
            return epilationmedicaltypes;
        }


        [HttpGet]
        [Route("/api/epilationmedicaltypes/getepilationmedicaltypebyid/{id}")]
        public async Task<ActionResult<Epilationmedicaltype>> GetEpilationmedicaltypeById(int id)
        {
            var epilationmedicaltype = await _iEpilationMedicalTypesData.GetEpilationmedicaltypeById(id);
            if (epilationmedicaltype == null)
            {
                return NotFound();
            }
            return epilationmedicaltype;
        }


        [HttpPut]
        [Route("/api/epilationmedicaltypes/updateepilationmedicaltype/{id}")]
        public async Task<IActionResult> UpdateEpilationmedicaltype(int id, Epilationmedicaltype epilationmedicaltype)
        {
            if (id != epilationmedicaltype.Idepilationmedicaltype)
            {
                return NoContent();
            }
            var res = await _iEpilationMedicalTypesData.UpdateEpilationmedicaltype(id, epilationmedicaltype);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("/api/epilationmedicaltypes/createepilationmedicaltype")]
        public async Task<ActionResult<Epilationmedicaltype>> CreateEpilationmedicaltype(Epilationmedicaltype epilationmedicaltype)
        {
            var result = await _iEpilationMedicalTypesData.CreateEpilationmedicaltype(epilationmedicaltype);
            if (result)
            {
                return CreatedAtAction("CreateEpilationmedicaltype", new { id = epilationmedicaltype.Idepilationmedicaltype }, epilationmedicaltype);
            }
            else
            {
                return BadRequest();
            }
            
        }


        [HttpDelete]
        [Route("/api/epilationmedicaltypes/deleteepilationmedicaltype/{id}")]
        public async Task<IActionResult> DeleteEpilationmedicaltype(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var res = await _iEpilationMedicalTypesData.DeleteEpilationmedicaltype(id);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(res);
        }
    }
}
