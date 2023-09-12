using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.WaxingTypes;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaxingtypesController : ControllerBase
    {
        private readonly IWaxingTypesData _iWaxingTypesData;

        public WaxingtypesController(IWaxingTypesData waxingTypesData)
        {
            _iWaxingTypesData = waxingTypesData;
        }

        
        [HttpGet]
        [Route("/api/waxingtypes/getallwaxingtypes")]
        public async Task<ActionResult<IEnumerable<Waxingtype>>> GetAllWaxingtypes()
        {
            var waxingtypes = await _iWaxingTypesData.GetAllWaxingtypes();
            if (waxingtypes == null)
            {
                return NotFound();
            }
            return waxingtypes;
        }

        
        [HttpGet]
        [Route("/api/waxingtypes/getwaxingtypebyid/{id}")]
        public async Task<ActionResult<Waxingtype>> GetWaxingtypeById(int id)
        {
            var waxingtype = await _iWaxingTypesData.GetWaxingtypeById(id);
            if (waxingtype == null)
            {
                return NotFound();
            }
            return waxingtype;
        }

      
        [HttpPut]
        [Route("/api/waxingtypes/updatewaxingtype/{id}")]
        public async Task<IActionResult> UpdateWaxingtype(int id, Waxingtype waxingtype)
        {
            if (id != waxingtype.Idwaxingtype)
            {
                return NoContent();
            }
            var res = await _iWaxingTypesData.UpdateWaxingtype(id, waxingtype);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

      
        [HttpPost]
        [Route("/api/waxingtypes/createwaxingtype")]
        public async Task<ActionResult<Waxingtype>> CreateWaxingtype(Waxingtype waxingtype)
        {
            var result = await _iWaxingTypesData.CreateWaxingtype(waxingtype);
            if (result)
            {
                return CreatedAtAction("CreateWaxingtype", new { id = waxingtype.Idwaxingtype }, waxingtype);
            }
            else
            {
                return BadRequest();
            }
           
        }

       
        [HttpDelete]
        [Route("/api/waxingtypes/deletewaxingtype/{id}")]
        public async Task<IActionResult> DeleteWaxingtype(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var res = await _iWaxingTypesData.DeleteWaxingtype(id);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(res);
        }
    }
}
