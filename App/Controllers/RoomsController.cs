using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.GeneratedModels;
using Services.DTO;
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

        [HttpPost]
        [Route("/api/rooms/changerooms")]
        public async Task<IActionResult> ChangeRooms(List<List<RoomEmployeeDto>> rooms) 
        {
            var res = await _iRoomsData.ChangeRooms(rooms);
            return Ok("ok");
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
        [Route("/api/rooms/getallroomswithtypes")]
        public async Task<ActionResult<IEnumerable<List<RoomEmployeeDto>>>> GetAllRoomsWithTypes()
        {
            var rooms = await _iRoomsData.GetAllRoomsWithTypes();
            if (rooms == null)
            {
                return NotFound();
            }
            return rooms;
        }

        [HttpGet]
        [Route("/api/rooms/getallnamerooms")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllNameRooms()
        {
            var rooms = await _iRoomsData.GetAllNameRooms();
            if (rooms == null)
            {
                return NotFound();
            }
            return rooms;
        }

        [HttpGet]
        [Route("/api/rooms/getallfieldsforroom")]
        public async Task<ActionResult<IEnumerable<RoomFieldsDto>>> GetAllFieldsForRoom()
        {
            var rooms = await _iRoomsData.GetAllFieldsForRoom();
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
                return NoContent();
            }

            var res = await _iRoomsData.UpdateRoom(id, room);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("/api/rooms/createroom")]
        public async Task<ActionResult<Room>> CreateRoom(Room room)
        {
            room.Isshow = true;
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
