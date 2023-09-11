using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.Employees;
using Services.TreatmentsType;
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
        private readonly ITreatmentsTypeData _iTreatmentsTypeData;
        private readonly IEmployeesData _iEmployeesData;

        public RoomsData(ClinicDBContext context, ITreatmentsTypeData treatmentsTypeData, IEmployeesData employeesData)
        {
            _context = context;
            _iTreatmentsTypeData = treatmentsTypeData;
            _iEmployeesData = employeesData;
        }

        public async Task<bool> CreateRoom(Room room)
        {
            room.Isshow = true;
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
            return await _context.Rooms.Where(r => r.Isshow == true).ToListAsync();
        }

        public async Task<string?> GetNameRoom(int id)
        {
            var room = await _context.Rooms.Where(r => r.Idroom == id && r.Isshow == true).FirstOrDefaultAsync();
            return room?.Nameroom;
        }

        public async Task<List<string>?> GetTreatmentsForRoom(int id)
        {
            var room = await GetRoomById(id);
            var list = new List<string>();
            if (room == null || room.Isshow == false || room.Treatmentstype == null)
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
                if(room == null || room.Nameroom == null || room.Isshow == false)
                {
                    continue;
                }
                list.Add(room.Nameroom);
            }
            return list;
        }

        public async Task<bool> ChangeRooms(List<List<RoomEmployeeDto>> rooms)
        {
            foreach(var room in rooms)
            {
                var size = room.Count();
                var newRoom = new Room();
                newRoom.Nameroom = room[size - 2].c;
                int idRoom = 0;
                if (room[size - 1].c != null && int.TryParse(room[size - 1].c, out idRoom))
                {
                }
                else
                {
                    idRoom = 0;
                }
                room.RemoveAt(size - 1);
                var list = new List<string>();
                var t = await _iTreatmentsTypeData.GetlistTreatmentstypes();
                var types = String.Join(",", t);
                foreach (var r in room)
                {
                    if(r == null || r?.name == null) {
                        continue;
                    }
                    if (r.name != "Room Name" && !types.Contains(r.name))
                    {
                        var treatmentstype = new Treatmentstype();
                        treatmentstype.Nametreatment = r.name;
                        var flage = await _iTreatmentsTypeData.CreateTreatmentstype(treatmentstype);
                    }
                    if(r.c == "true")
                    {
                        list.Add(r.name);
                    }
                }
                newRoom.Treatmentstype = String.Join(",", list);
                var res = await GetRoomById(idRoom);
                if(res != null) // room is exist - go to update...
                {
                   // newRoom.Idroom = res.Idroom;
                    if(newRoom.Nameroom != res.Nameroom || newRoom.Treatmentstype != res.Treatmentstype)
                    {
                        res.Nameroom = newRoom.Nameroom;
                        res.Treatmentstype = newRoom.Treatmentstype;
                        var result = await UpdateRoom(res.Idroom, res);
                    }
                    else
                    {
                        continue;
                    }
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
            room.Isshow = room.Isshow == false ? false : true;
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
                if(room != null &&  room.Nameroom == name && room.Isshow == true)
                {
                    return room;
                }
            }
            return null;
        }

        public async Task<List<List<RoomEmployeeDto>>> GetAllRoomsWithTypes()
        {
            var rooms = await GetAllRooms();
            var types = await _iTreatmentsTypeData.GetlistTreatmentstypes();
            var listRooms = new List<List<RoomEmployeeDto>>();
            foreach(var room in rooms)
            {
                if (room == null || room.Treatmentstype == null || room.Isshow == false) { continue; }
                var list = new List<RoomEmployeeDto>();
                foreach(var item in types)
                { 
                    if(item == null) continue;
                    var roomDto = new RoomEmployeeDto();
                    roomDto.name = item;
                    if (room.Treatmentstype.Contains(item))
                    {
                        roomDto.c = "true";
                    }
                    else
                    {
                        roomDto.c = "false";
                    }
                    list.Add(roomDto);
                }
                var roomDto1 = new RoomEmployeeDto();
                roomDto1.c = room.Nameroom;
                roomDto1.name = "Room Name";
                list.Add(roomDto1);
                var roomdto = new RoomEmployeeDto();
                roomdto.name = "id";
                roomdto.c = "" + room.Idroom;
                list.Add(roomdto);
                listRooms.Add(list);
            }
            return listRooms;
        }

        public async Task<List<int>> GetAllRoomsIdForEmployee(int id)
        {
            var list = await _iEmployeesData.GetAllTreatmentsForEmployee(id);
            var listId = new List<int>();
            var rooms = await _context.Rooms.Where(r => r.Isshow == true).ToListAsync();
            if (list == null)
            {
                return listId;
            }
            
            foreach(var room in rooms)
            {
                var flag = false;
                foreach(var i in list)
                {
                    if(room == null && i == null)
                    {
                        continue;
                    }
                    if (room?.Treatmentstype != null && room.Treatmentstype.Contains(i))
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    listId.Add(room.Idroom);
                }
            }
            return listId;
        }

        public async Task<int> GetRoomIdByName(string? name)
        {
            var room = await GetRoomByName(name);
            return room == null ? 0 : room.Idroom;
        }

        public async Task<List<RoomDetails>> GetAllNamesWithIdRooms()
        {
            var rooms = await GetAllRooms();
            var list = new List<RoomDetails>();
            foreach(var room in rooms)
            {
                if(room == null)
                {
                    continue;
                }
                var r = new RoomDetails();
                r.id = room.Idroom;
                r.name = room.Nameroom;
                list.Add(r);
            }
            return list;
        }

        public async Task<bool> DeleteRoomById(int id)
        {
            var room = await GetRoomById(id);
            if(room == null)
            {
                return false;
            }
            room.Isshow = false;
            var isOk = await UpdateRoom(room.Idroom, room);
            return isOk;
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
