using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.Employees;
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
        private readonly IEmployeesData _iEmployeesData;

        public RoomsData(ClinicDBContext context, IEmployeesData employeesData)
        {
            _context = context;
            _iEmployeesData = employeesData;
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

        public async Task<string?> GetNameRoom(int id)
        {
            var room = await _context.Rooms.Where(r => r.Idroom == id).FirstOrDefaultAsync();
            return room?.Nameroom;
        }

        public async Task<List<string?>> GetTreatmentsForRoom(int id)
        {
            var room = await GetRoomById(id);
            var list = new List<string?>();
            if(room?.Laser == true)
            {
                list.Add("Laser");
            }
            if (room?.Waxing == true)
            {
                list.Add("Waxing");
            }
            if (room?.Electrolysis == true)
            {
                list.Add("Electrolysis");
            }
            if (room?.Advancedelectrolysis == true)
            {
                list.Add("Advancedelectrolysis");
            }
            return list;
        }

        public async Task<List<Employee>> GetAllEmployeesForRoom(int id)
        {
            var list = await GetTreatmentsForRoom(id);
            var employees = await _iEmployeesData.GetAllEmployeesForRoom(list);
            return employees.ToList();
        }

        public async Task<List<RoomFieldsDto>> GetAllFieldsForRoom()
        {
            var rooms = await GetAllRooms();
            var listRoomFieldsDto = new List<RoomFieldsDto>();
            int i = 0;
            foreach(var room in rooms)
            {
                if (room == null)
                {
                    continue;
                }
                var roomFieldsDto = new RoomFieldsDto();
                var list = new List<string>();
                roomFieldsDto.Id = i++;
                roomFieldsDto.IdRoom = room.Idroom;
                roomFieldsDto.nameRoom = room.Nameroom;

                if (room?.Laser == true)
                {
                    list.Add("Laser");
                }
                if (room?.Waxing == true)
                {
                    list.Add("Waxing");
                }
                if (room?.Electrolysis == true)
                {
                    list.Add("Electrolysis");
                }
                if (room?.Advancedelectrolysis == true)
                {
                    list.Add("Advancedelectrolysis");
                }
                roomFieldsDto.fields = list;
                listRoomFieldsDto.Add(roomFieldsDto);
            }
            return listRoomFieldsDto;
        }

    }
}
