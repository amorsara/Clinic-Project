using Repository.GeneratedModels;
using Services.DTO;
using Services.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FuncRef
{
    public class RoomRef : IRoomsRef
    {
        private readonly IRoomsData _iRoomsData;

        public RoomRef(IRoomsData roomsData)
        {
            _iRoomsData = roomsData;
        }

        public async Task<List<Employee>> GetAllEmployeesForRoom(int id)
        {
            return await _iRoomsData.GetAllEmployeesForRoom(id);
        }

        public async Task<List<Room>> GetAllRooms()
        {
            return await _iRoomsData.GetAllRooms();
        }

        public async Task<string?> GetNameRoom(int id)
        {
            return await _iRoomsData.GetNameRoom(id);
        }

        public async Task<List<string>?> GetTreatmentsForRoom(int id)
        {
            return await _iRoomsData.GetTreatmentsForRoom(id);
        }
    }
}
