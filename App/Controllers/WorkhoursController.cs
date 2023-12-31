﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.WorkHours;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkhoursController : ControllerBase
    {

        private readonly IWorkHoursData _iWorkHoursData;

        public WorkhoursController( IWorkHoursData workHoursData)
        {
            _iWorkHoursData = workHoursData;
        }

        [HttpGet]
        [Route("/api/workhours/getallworkhour")]
        public async Task<ActionResult<IEnumerable<Workhour>>> GetAllWorkhours()
        {
            var workhours = await _iWorkHoursData.GetAllWorkHours();
            if (workhours == null)
            {
                return NotFound();
            }
            return workhours;
        }

        [HttpGet]
        [Route("/api/workhours/getworkhourbyid/{id}")]
        public async Task<ActionResult<Workhour>> GetWorkhourById(int id)
        {
            var workhour = await _iWorkHoursData.GetWorkHourById(id);
            if (workhour == null)
            {
                return NotFound();
            }
            return workhour;
        }

        [HttpPut]
        [Route("/api/workhours/updateworkhour/{id}")]
        public async Task<IActionResult> UpdateWorkhour(int id, Workhour workhour)
        {
            if (id != workhour.Idworkhour)
            {
                return NoContent();
            }
            var res = await _iWorkHoursData.UpdateWorkhour(id, workhour);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpPost]
        [Route("/api/workhours/createworkhour")]
        public async Task<ActionResult<Workhour>> CreateWorkhour(Workhour workhour)
        {
            var result = await _iWorkHoursData.CreateWorkHour(workhour);
            if (result)
            {
                return CreatedAtAction("CreateWorkhour", new { id = workhour.Idworkhour }, workhour);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("/api/workhours/deleteworkhour/{id}")]
        public async Task<IActionResult> DeleteWorkhour(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var res = await _iWorkHoursData.DeleteWorkhour(id);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("/api/workhours/deleteshiftByHour")]
        public async Task<IActionResult> DeleteshiftByHour(DeleteShiftDto deleteShift)
        {
            if (deleteShift.idWorker == 0 || deleteShift.day == 0)
            {
                return BadRequest();
            }
            var res = await _iWorkHoursData.DeleteShift(deleteShift.idWorker, deleteShift.day, deleteShift.start);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(res);

        }
    }
}
