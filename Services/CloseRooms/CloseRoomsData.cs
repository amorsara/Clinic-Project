using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Appointments;
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
        private readonly IAppointmentsData _iAppointmentsData;

        public CloseRoomsData(ClinicDBContext context, IRoomsData roomsData, IAppointmentsData appointmentsData)
        {
            _context = context;
            _iRoomsData = roomsData;
            _iAppointmentsData = appointmentsData;
        }

        public Task<bool> CancelAppointment(DateOnly date)
        {
            throw new NotImplementedException();
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

            var isExsists = CloseroomExists(closeRoomDto.idClose);
            if (isExsists)
            {
                return false;
            }

            var closeRoom = new Closeroom();
            closeRoom.Starttime = closeRoomDto.startTime;
            closeRoom.Endtime = closeRoomDto.endTime;
            closeRoom.Startdate = closeRoomDto.startDate;
            closeRoom.Enddate = closeRoomDto.endDate;
            closeRoom.Reason = closeRoomDto.reason;
            
            var names = closeRoomDto.name;
            var list = new List<int>();
            if(names != null)
            {
                foreach (var name in names)
                {
                    var id = await _iRoomsData.GetRoomIdByName(name);
                    if(id != 0)
                    {
                        list.Add(id);
                    }
                }
                closeRoom.Idrooms = list.Count() > 0 ? String.Join(",",list) : null;
            }
           
            await _context.AddAsync(closeRoom);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk == true)
            {
                var idRooms = new List<string>();
                if (closeRoom.Idrooms != null)
                {
                    idRooms = closeRoom.Idrooms.Split(",").ToList();
                }
                foreach(var id in idRooms)
                {
                    var cancel = await _iAppointmentsData.CancelAppointment(int.Parse(id), 0, closeRoom.Startdate, closeRoom.Enddate, closeRoom.Starttime, closeRoom.Endtime, true);
                    if (cancel == false)
                    {
                        return false;
                    }
                }
                
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

            var closeRoom = await GetCloseroomById(id);
            if (closeRoom == null)
            {
                return false;
            }

            _context.Closerooms.Remove(closeRoom);
            await _context.SaveChangesAsync();

            var cancel = await _iAppointmentsData.CancelAppointment(id, 0, closeRoom.Startdate, closeRoom.Enddate, closeRoom.Starttime, closeRoom.Endtime, false);
            if (cancel == false)
            {
                return false;
            }

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
                closeRoomDto.idClose = item.Idcloseroom;

                var idRooms = item.Idrooms?.Split(",").ToList();
                var names = new List<string>();
                if(idRooms != null)
                {
                    foreach (var id in idRooms)
                    {
                        var name = await _iRoomsData.GetNameRoom(int.Parse(id));
                        if(name != null)
                        {
                            names.Add(name);
                        }
                    }
                }            

                closeRoomDto.name = names;               
                closeRoomDto.startDate = item.Startdate;
                closeRoomDto.endDate = item.Enddate;
                closeRoomDto.startTime = item.Starttime;
                closeRoomDto.endTime = item.Endtime;
                closeRoomDto.reason = item.Reason;

                list.Add(closeRoomDto);
            }
            return list;
        }

        public async Task<List<CloseEventsDto>> GetCloseEventsForRoomsForWeek(DateOnly date)
        {
            var date2 = (DateOnly)date;
            var closeRooms = await _context.Closerooms.Where(c => c.Startdate >= date && c.Startdate <= date2.AddDays(5) || c.Enddate >= date && c.Enddate <= date.AddDays(5)).ToListAsync();

            var listEvents = new List<CloseEventsDto>();
            foreach(var c in closeRooms)
            {
                if(c == null)
                {
                    continue;
                }

                var rooms = c.Idrooms?.Split(",").ToList();
                if(rooms == null)
                {
                    continue;
                }

                foreach(var id in rooms)
                {
                    var room = await _iRoomsData.GetRoomById(int.Parse(id));
                    if(room == null)
                    {
                        continue;
                    }
                    if(c.Startdate != null && c.Enddate != null)
                    {
                        var d1 = (DateOnly)c.Startdate;
                        var d2 = (DateOnly)c.Enddate;
                        while(d1 <= d2)
                        {
                            if(d1 < date || d1 > date2.AddDays(5))
                            {
                                continue;
                            }
                            var closeEvent = new CloseEventsDto();
                            closeEvent.id = c.Idcloseroom;
                            closeEvent.idRoom = room.Idroom;
                            closeEvent.date = d1;
                            closeEvent.nameEvent = c.Reason;
                            closeEvent.startHour = c.Starttime;
                            closeEvent.endTime = c.Endtime;
                            listEvents.Add(closeEvent);
                            d1 = d1.AddDays(1);
                        }                   
                    }                 
                }
            }
            return listEvents;
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
            closeroom.Idrooms = closeroom.Idrooms?.Count() > 0 ? String.Join(",", closeroom.Idrooms) : null;

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

        //public async Task<List<Closeroom>> GetAllCloseroomsForId(int id, DateOnly date)
        //{
        //    var closerooms = await GetCloserooms();
        //    var nameRoom = await _iRoomRef.GetNameRoom(id);
        //    nameRoom = nameRoom != null ? nameRoom : " ";
        //    var list = new List<Closeroom>();
        //    foreach (var item in closerooms)
        //    {
        //        //var i = "" + id;

        //        if (item != null && item.Roomsname?.Contains(nameRoom) == true && (item.Startdate >= date && item.Startdate <= date.AddDays(5) || item.Enddate >= date && item.Enddate <= date.AddDays(5)))
        //        {
        //            list.Add(item);
        //        }

        //    }
        //    return list;
        //}

    }
}


