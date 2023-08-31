using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.TempCloseEmployees;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempcloseemployeesController : ControllerBase
    {
        private readonly ITempCloseEmployeesData _iTempCloseEmployeesData;

        public TempcloseemployeesController(ITempCloseEmployeesData tempCloseEmployeesData)
        {
            _iTempCloseEmployeesData = tempCloseEmployeesData;
        }

        [HttpGet]
        [Route("/api/tempcloseemployees/getalltempcloseemployees")]
        public async Task<ActionResult<IEnumerable<TempCloseEmployeeDto>>> GetAllTempcloseemployees()
        {
            var tempcloseemployee = await _iTempCloseEmployeesData.GetAllTempcloseemployees();
            if (tempcloseemployee == null)
            {
                return NotFound();
            }
            return tempcloseemployee;
        }

        [HttpGet]
        [Route("/api/tempcloseemployees/getalltempcloseemployeesbyid/{id}")]
        public async Task<ActionResult<IEnumerable<TempCloseEmployeeDto>>> GetAllTempcloseemployeesById(int id)
        {
            var tempcloseemployee = await _iTempCloseEmployeesData.GetAllTempcloseemployeesById(id);
            if (tempcloseemployee == null)
            {
                return NotFound();
            }
            return tempcloseemployee;
        }

        [HttpGet]
        [Route("/api/tempcloseemployees/gettempcloseemployeebyid/{id}")]
        public async Task<ActionResult<Tempcloseemployee>> GetTempcloseemployeeById(int id)
        {
            var tempcloseemployee = await _iTempCloseEmployeesData.GetTempcloseemployeeById(id);
            if (tempcloseemployee == null)
            {
                return NotFound();
            }
            return tempcloseemployee;
        }

        [HttpGet]
        [Route("/api/tempcloseemployees/updatestatustempcloseemployee/{id}/{status}")]
        public async Task<ActionResult> UpdateStatusTempcloseemployee(int id, bool status)
        {
            var res = await _iTempCloseEmployeesData.UpdateStatusTempcloseemployee(id, status);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(true);
        }

        [HttpPut]
        [Route("/api/tempcloseemployees/updatetempcloseemployee/{id}")]
        public async Task<IActionResult> UpdateTempcloseemployee(int id, TempCloseEmployeeDto tempcloseemployee)
        {
            if (id != tempcloseemployee.id)
            {
                return NoContent();
            }
            var res = await _iTempCloseEmployeesData.UpdateTempcloseemployeeWrapper(id, tempcloseemployee);
            
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("/api/tempcloseemployees/createtempcloseemployee")]
        public async Task<ActionResult<TempCloseEmployeeDto>> CreateTempcloseemployee(TempCloseEmployeeDto tempcloseemployee)
        {
            var result = await _iTempCloseEmployeesData.CreateTempcloseemployee(tempcloseemployee);
            if (result)
            {
                return CreatedAtAction("CreateTempcloseemployee", new { id = tempcloseemployee.id }, tempcloseemployee);
            }
            else
            {
                return BadRequest();
            }
         
        }

        [HttpDelete]
        [Route("/api/tempcloseemployees/deletetempcloseemployee/{id}")]
        public async Task<IActionResult> DeleteTempcloseemployee(int id)
        {
            var tempcloseemployee = await _iTempCloseEmployeesData.DeleteTempcloseemployee(id);
            if (tempcloseemployee == false)
            {
                return BadRequest();
            }

            return Ok(true);
        }

    }
}


