using Repository.GeneratedModels;
using Services.DTO;
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
        Task<List<string>> GetAllNameRooms();
        Task<List<string?>> GetTreatmentsForRoom(int id); 
        Task<List<Employee>> GetAllEmployeesForRoom(int id);
        Task<List<RoomFieldsDto>> GetAllFieldsForRoom();
        bool RoomExists(int id);
    }
}
