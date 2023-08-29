using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.CloseRooms;
using Services.DTO;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloseroomsController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly ICloseRoomsData _iCloseRoomsData;

        public CloseroomsController(ClinicDBContext context, ICloseRoomsData closeRoomsData)
        {
            _context = context;
            _iCloseRoomsData = closeRoomsData;
        }


        [HttpGet]
        [Route("/api/closerooms/getcloserooms")]
        public async Task<ActionResult<IEnumerable<Closeroom>>> GetCloserooms()
        {
            var closeroom = await _iCloseRoomsData.GetCloserooms();
            if (closeroom == null)
            {
                return BadRequest();
            }
            return closeroom;
        }

        [HttpGet]
        [Route("/api/closerooms/getallcloserooms")]
        public async Task<ActionResult<List<CloseRoomDto>>> GetAllCloserooms()
        {
            var closerooms = await _iCloseRoomsData.GetAllCloserooms();
            if (closerooms == null)
            {
                return BadRequest();
            }
            return closerooms;
        }

        [HttpGet]
        [Route("/api/closerooms/getcloseroombyid/{id}")]
        public async Task<ActionResult<Closeroom>> GetCloseroomById(int id)
        {
            var closeroom = await _iCloseRoomsData.GetCloseroomById(id);
            if (closeroom == null)
            {
                return NotFound();
            }
            return closeroom;
        }


        [HttpPut]
        [Route("/api/closerooms/updatecloseroom")]
        public async Task<IActionResult> UpdateCloseroom(int id, Closeroom closeroom)
        {
            if (id != closeroom.Idcloseroom)
            {
                return NoContent();
            }
            var res = await _iCloseRoomsData.UpdateCloseroom(id, closeroom);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

 
        [HttpPost]
        [Route("/api/closerooms/createcloseroom")]
        public async Task<ActionResult<Closeroom>> CreateCloseroom(CloseRoomDto closeroom)
        {
            var result = await _iCloseRoomsData.CreateCloseroom(closeroom);
            if (result)
            {
                return CreatedAtAction("CreateCloseroom", new { id = closeroom.idClose }, closeroom);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("/api/closerooms/deletecloseroom/{id}")]
        public async Task<IActionResult> DeleteCloseroom(int id)
        {
            var isOk = await _iCloseRoomsData.DeleteCloseroom(id);
            if (isOk == false)
            {
                return NotFound();
            }
            return Ok(isOk);
        }

    }
}
