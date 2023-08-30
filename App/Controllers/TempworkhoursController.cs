using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.TempWorkHours;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempworkhoursController : ControllerBase
    {

        private readonly ITempWorkHourData _iTempWorkHourData;

        public TempworkhoursController(ITempWorkHourData tempWorkHourData)
        {
            _iTempWorkHourData = tempWorkHourData;
        }

        [HttpGet]
        [Route("/api/tempworkhours/getalltempworkhours")]
        public async Task<ActionResult<IEnumerable<Tempworkhour>>> GetAllTempworkhours()
        {
            var tempworkhour = await _iTempWorkHourData.GetAllTempworkhours();
            if (tempworkhour == null)
            {
                return NotFound();
            }
            return tempworkhour;
        }

        [HttpGet]
        [Route("/api/tempworkhours/getalltempworkhoursforid/{id}")]
        public async Task<ActionResult<IEnumerable<Tempworkhour>>> GetAllTempworkhoursForId(int id)
        {
            var tempworkhour = await _iTempWorkHourData.GetAllTempworkhoursForId(id);
            if (tempworkhour == null)
            {
                return NotFound();
            }
            return tempworkhour;
        }

        [HttpGet]
        [Route("/api/tempworkhours/gettempworkhourbyid/{id}")]
        public async Task<ActionResult<Tempworkhour>> GetTempworkhourById(int id)
        {
            var tempworkhour = await _iTempWorkHourData.GetTempworkhourById(id);
            if (tempworkhour == null)
            {
                return NotFound();
            }
            return tempworkhour;
        }

        [HttpGet]
        [Route("/api/tempworkhours/updatestatustempworkhourbyid/{id}")]
        public async Task<ActionResult<Tempworkhour>> UpdateStatusTempworkhourById(int id)
        {
            var tempworkhour = await _iTempWorkHourData.UpdateStatusTempworkhour(id);
            if (tempworkhour == false)
            {
                return BadRequest();
            }
            return Ok(true);
        }


        [HttpPut]
        [Route("/api/tempworkhours/updatetempworkhour/{id}")]
        public async Task<IActionResult> UpdateTempworkhour(int id, Tempworkhour tempworkhour)
        {
            if (id != tempworkhour.Idtempworkhour)
            {
                return NoContent();
            }
            var res = await _iTempWorkHourData.UpdateTempworkhour(id, tempworkhour);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("/api/tempworkhours/createtempworkhour")]
        public async Task<ActionResult<Tempworkhour>> CreateTempworkhour(Tempworkhour tempworkhour)
        { 
            var result = await _iTempWorkHourData.CreateTempworkhour(tempworkhour);
            if (result)
            {
                return CreatedAtAction("CreateTempworkhour", new { id = tempworkhour.Idtempworkhour }, tempworkhour);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("/api/tempworkhours/deletetempworkhour/{id}")]
        public async Task<IActionResult> DeleteTempworkhour(int id)
        {
            var tempworkhour = await _iTempWorkHourData.DeleteTempworkhour(id);
            if (tempworkhour == false)
            {
                return BadRequest();
            }

            return Ok(tempworkhour);
        }
    }
}
