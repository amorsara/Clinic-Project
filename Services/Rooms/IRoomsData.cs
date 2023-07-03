using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Rooms
{
    public interface IRoomsData
    {
        Task<List<Room>> GetAllRooms();
        Task<Room?> GetRoomById(int id);
        Task<bool> CreateRoom(Room room);
        bool RoomExists(int id);
    }
}
