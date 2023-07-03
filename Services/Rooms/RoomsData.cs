using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Rooms
{
    public class RoomsData : IRoomsData
    {

        private readonly ClinicDBContext _context;

        public RoomsData(ClinicDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRoom(Room room)
        {
            var isExsists = RoomExists(room.Idroom);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(room);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public bool RoomExists(int id)
        {
            var room = _context.Rooms.Where(r => r.Idroom == id).FirstOrDefault();
            if (room == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Room?> GetRoomById(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            return room;
        }

        public async Task<List<Room>> GetAllRooms()
        {
            return await _context.Rooms.ToListAsync();
        }
    }
}
