using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Rooms;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly IRoomsData _iRoomsData;

        public RoomsController(ClinicDBContext context, IRoomsData roomsData)
        {
            _context = context;
            _iRoomsData = roomsData;
        }

        [HttpGet]
        [Route("/api/rooms/getallrooms")]
        public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
        {
            var rooms = await _iRoomsData.GetAllRooms();
            if (rooms == null)
            {
                return NotFound();
            }
            return rooms;
        }

        [HttpGet]
        [Route("/api/rooms/getroombyid/{id}")]
        public async Task<ActionResult<Room>> GetRoomById(int id)
        {
            var room = await _iRoomsData.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }
            return room;
        }

        [HttpPut]
        [Route("/api/rooms/updateroom/{id}")]
        public async Task<IActionResult> UpdateRoom(int id, Room room)
        {
            if (id != room.Idroom)
            {
                return BadRequest();
            }

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_iRoomsData.RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Route("/api/rooms/createroom")]
        public async Task<ActionResult<Room>> CreateRoom(Room room)
        {
            var result = await _iRoomsData.CreateRoom(room);
            if (result)
            {
                return CreatedAtAction("CreateRoom", new { id = room.Idroom }, room);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("/api/rooms/deleteroom/{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            if (_context.Rooms == null)
            {
                return NotFound();
            }
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
