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

        public async Task<List<string>?> GetTreatmentsForRoom(int id)
        {
            var room = await GetRoomById(id);
            var list = new List<string>();
            if (room == null || room.Treatmentstype == null)
            {
                return list;
            }
            list = room.Treatmentstype.Split(',').ToList();
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
                roomFieldsDto.fields = room?.Treatmentstype?.Split(',').ToList();
                listRoomFieldsDto.Add(roomFieldsDto);
            }
            return listRoomFieldsDto;
        }

        public async Task<List<string>> GetAllNameRooms()
        {
            var rooms = await GetAllRooms();
            var list = new List<string>();
            foreach(var room in rooms)
            {
                if(room == null || room.Nameroom == null)
                {
                    continue;
                }
                list.Add(room.Nameroom);
            }
            return list;
        }

        public async Task<bool> ChangeRooms(List<List<RoomDto>> rooms)
        {
            foreach(var room in rooms)
            {
                var size = room.Count();
                var newRoom = new Room();
                newRoom.Nameroom = room[size - 1].c;
                var list = new List<string>();
                foreach(var r in room)
                {
                    if(r.c == "true" && r.name != null)
                    {
                        list.Add(r.name);
                    }
                }
                newRoom.Treatmentstype = String.Join(",", list);
                var res = await GetRoomByName(newRoom.Nameroom);
                if(res != null) // room is exist - go to update...
                {
                    newRoom.Idroom = res.Idroom;
                    var result = await UpdateRoom(res.Idroom, newRoom);
                }
                else // room isnt exist - go to create...
                {
                    var s = await CreateRoom(newRoom);
                }
            }
            return true;
        }

        public async Task<bool> UpdateRoom(int id, Room room)
        {
            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<Room?> GetRoomByName(string? name)
        {
            var rooms = await GetAllRooms();
            foreach(var room in rooms)
            {
                if(room != null &&  room.Nameroom == name)
                {
                    return room;
                }
            }
            return null;
        }
    }
}



//if(room?.Laser == true)
//{
//    list.Add("Laser");
//}
//if (room?.Waxing == true)
//{
//    list.Add("Waxing");
//}
//if (room?.Electrolysis == true)
//{
//    list.Add("Electrolysis");
//}
//if (room?.Advancedelectrolysis == true)
//{
//    list.Add("Advancedelectrolysis");
//}

//if(room?.Laser == true)
//{
//    list.Add("Laser");
//}
//if (room?.Waxing == true)
//{
//    list.Add("Waxing");
//}
//if (room?.Electrolysis == true)
//{
//    list.Add("Electrolysis");
//}
//if (room?.Advancedelectrolysis == true)
//{
//    list.Add("Advancedelectrolysis");
//}
