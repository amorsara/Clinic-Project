using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CloseRooms
{
    public class CloseRoomsData : ICloseRoomsData
    {
        private readonly ClinicDBContext _context;
        private readonly IRoomsData _iRoomsData;

        public CloseRoomsData(ClinicDBContext context, IRoomsData roomsData)
        {
            _context = context;
            _iRoomsData = roomsData;
        }

        public bool CloseroomExists(int id)
        {
            var closeroom = _context.Closerooms.Where(c => c.Idcloseroom == id).FirstOrDefault();
            if (closeroom == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CreateCloseroom(CloseRoomDto closeRoomDto)
        {
            if(closeRoomDto == null)
            {
                return false;
            }
            var closeRoom = new Closeroom();
            closeRoom.Starttime = closeRoomDto.startTime;
            closeRoom.Endtime = closeRoomDto.endTime;
            closeRoom.Startdate = closeRoomDto.startDate;
            closeRoom.Enddate = closeRoomDto.endDate;
            closeRoom.Reason = closeRoomDto.reason;
            closeRoom.Roomsid = closeRoomDto.idRoom?.Count() > 0 ? String.Join(",", closeRoomDto.idRoom) : null;
            var isExsists = CloseroomExists(closeRoomDto.idClose);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(closeRoom);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteCloseroom(int id)
        {
            if (_context.Closerooms == null)
            {
                return false;
            }

            var closeroom = await GetCloseroomById(id);
            if (closeroom == null)
            {
                return false;
            }

            var ok = await _context.Closerooms.FindAsync(closeroom.Idcloseroom);
            if (ok == null)
            {
                return false;
            }

            _context.Closerooms.Remove(closeroom);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<CloseRoomDto>> GetAllCloserooms()
        {
            var closerooms = await GetCloserooms();
            var list = new List<CloseRoomDto>();
            foreach(var item in closerooms)
            {
                if(item == null)
                {
                    continue;
                }
                var closeRoomDto = new CloseRoomDto();
                var listName = new List<string>();
                closeRoomDto.idClose = item.Idcloseroom;
                closeRoomDto.idRoom = item.Roomsid?.Split(",").ToList();
                if(closeRoomDto.idRoom != null)
                {
                    foreach (var i in closeRoomDto.idRoom)
                    {
                        var name = await _iRoomsData.GetNameRoom(int.Parse(i));
                        if(name != null)
                        {
                            listName.Add(name);
                        }
                        
                    }

                    closeRoomDto.name = listName;
                }
                
                closeRoomDto.startDate = item.Startdate;
                closeRoomDto.endDate = item.Enddate;
                closeRoomDto.startTime = item.Starttime;
                closeRoomDto.endTime = item.Endtime;
                closeRoomDto.reason = item.Reason;

                list.Add(closeRoomDto);
            }
            return list;
        }

        public async Task<Closeroom?> GetCloseroomById(int id)
        {
            var closeroom = await _context.Closerooms.FindAsync(id);
            return closeroom;
        }

        public async Task<List<Closeroom>> GetCloserooms()
        {
            return await _context.Closerooms.ToListAsync();
        }

        public async Task<bool> UpdateCloseroom(int id, Closeroom closeroom)
        {
            var closeRoom = await GetCloseroomById(id);
            if (closeRoom == null)
            {
                return false;
            }

            closeRoom.Starttime = closeroom.Starttime;
            closeRoom.Endtime = closeroom.Endtime;
            closeRoom.Startdate = closeroom.Startdate;
            closeRoom.Enddate = closeroom.Enddate;
            closeRoom.Reason = closeroom.Reason;
            closeroom.Roomsid = closeroom.Roomsid?.Count() > 0 ? String.Join(",", closeroom.Roomsid) : null;

            _context.Entry(closeRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CloseroomExists(id))
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
    }
}
