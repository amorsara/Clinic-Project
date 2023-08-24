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
        Task<bool> ChangeRooms(List<List<RoomEmployeeDto>> rooms);
        Task<List<List<RoomEmployeeDto>>> GetAllRoomsWithTypes();
        Task<Room?> GetRoomById(int id);
        Task<bool> CreateRoom(Room room);
        Task<string?> GetNameRoom(int id);
        Task<Room?> GetRoomByName(string? name);
        Task<List<string>> GetAllNameRooms();
        Task<List<string>?> GetTreatmentsForRoom(int id); 
        Task<List<Employee>> GetAllEmployeesForRoom(int id);
        Task<List<RoomFieldsDto>> GetAllFieldsForRoom();
        Task<bool> UpdateRoom(int id, Room room);
        Task<bool> CloseRoom(int id);
        Task<bool> OpenRoom(int id);
        bool RoomExists(int id);
    }
}
