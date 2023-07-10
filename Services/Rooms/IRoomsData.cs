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
        Task<string?> GetNameRoom(int id);
        Task<List<string?>> GetTreatmentsForRoom(int id); 
        Task<List<Employee>> GetAllEmployeesForRoom(int id);
        bool RoomExists(int id);
    }
}
