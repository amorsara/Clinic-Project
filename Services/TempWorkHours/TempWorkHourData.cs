﻿using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TempWorkHours
{
    public class TempWorkHourData : ITempWorkHourData
    {
        private readonly ClinicDBContext _context;
        private readonly IRoomsData _iRoomsData;

        public TempWorkHourData(ClinicDBContext context, IRoomsData roomsData)
        {
            _context = context;
            _iRoomsData = roomsData;
        }

        public async Task<bool> CreateTempworkhour(TempWorkHourDto tempWorkHourDto)
        {
            var isExsists = TempworkhourExists(tempWorkHourDto.id);
            if (isExsists)
            {
                return false;
            }

            var tempworkhour = new Tempworkhour();
            tempworkhour.Starthouer = tempWorkHourDto.startHouer;
            tempworkhour.Endtime = tempWorkHourDto.endTime;
            tempworkhour.Date = tempWorkHourDto.date;
            tempworkhour.Day = tempWorkHourDto.day;
            tempworkhour.Status = tempWorkHourDto.status;
            tempworkhour.Idemployee = tempWorkHourDto.idWorker;
            var room = await _iRoomsData.GetRoomByName(tempWorkHourDto.nameRoom);
            if(room != null && room.Nameroom != null)
            {
                tempworkhour.Idroom = room.Idroom;
            }

            await _context.AddAsync(tempworkhour);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTempworkhour(int id)
        {
            if (_context.Tempworkhours == null)
            {
                return false;
            }
            var tempworkhour = await GetTempworkhourById(id);
            if(tempworkhour == null)
            {
                return false;
            }
            var ok = await _context.Tempworkhours.FindAsync(tempworkhour.Idtempworkhour);
            if (ok == null)
            {
                return false;
            }

            _context.Tempworkhours.Remove(tempworkhour);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Tempworkhour?> GetTempworkhourById(int id)
        {
            var tempworkhour = await _context.Tempworkhours.FindAsync(id);
            return tempworkhour;
        }

        public async Task<List<TempWorkHourDto>> GetAllTempworkhoursForWeek(DateOnly date)
        {
            var list = new List<TempWorkHourDto>();
            var date2 = (DateOnly)date;
            var workhours = await _context.Tempworkhours.Where(t => t.Date >= date && t.Date <= date2.AddDays(5)).ToListAsync();

            foreach (var workhour in workhours)
            {
                if(workhour == null || workhour.Status == false)
                {
                    continue;
                }
                var tempWorkHourDto = new TempWorkHourDto();
                tempWorkHourDto.id = workhour.Idtempworkhour;
                tempWorkHourDto.startHouer = workhour.Starthouer;
                tempWorkHourDto.endTime = workhour.Endtime;
                tempWorkHourDto.date = workhour.Date;
                tempWorkHourDto.day = workhour.Day;
                tempWorkHourDto.status = workhour.Status;
                tempWorkHourDto.idWorker = workhour.Idemployee;
                tempWorkHourDto.idroom = workhour.Idroom;
                tempWorkHourDto.nameRoom = await _iRoomsData.GetNameRoom(workhour.Idroom);
                list.Add(tempWorkHourDto);
            }
            return list;
        }

        public async Task<List<TempWorkHourDto>> GetAllTempworkhoursForId(int id)
        {
            var list = new List<TempWorkHourDto>();
            var workhours = await _context.Tempworkhours.Where(t => t.Idemployee == id && t.Status == true).ToListAsync();
            foreach (var workhour in workhours)
            {
                if (workhour == null)
                {
                    continue;
                }
                var tempWorkHourDto = new TempWorkHourDto();
                tempWorkHourDto.id = workhour.Idtempworkhour;
                tempWorkHourDto.startHouer = workhour.Starthouer;
                tempWorkHourDto.endTime = workhour.Endtime;
                tempWorkHourDto.date = workhour.Date;
                tempWorkHourDto.day = workhour.Day;
                tempWorkHourDto.status = workhour.Status;
                tempWorkHourDto.idWorker = workhour.Idemployee;
                tempWorkHourDto.idroom = workhour.Idroom;
                tempWorkHourDto.nameRoom = await _iRoomsData.GetNameRoom(workhour.Idroom);
                list.Add(tempWorkHourDto);
            }
            return list;
        }

        public bool TempworkhourExists(int id)
        {
            var tempworkhour = _context.Tempworkhours.Where(t => t.Idtempworkhour == id).FirstOrDefault();
            if (tempworkhour == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateTempworkhourWrapper(int id, TempWorkHourDto tempWorkHourDto)
        {
            var tempworkhour = await GetTempworkhourById(id);
            if(tempworkhour == null)
            {
                return false;
            }

            tempworkhour.Starthouer = tempWorkHourDto.startHouer;
            tempworkhour.Endtime = tempWorkHourDto.endTime;
            tempworkhour.Date = tempWorkHourDto.date;
            tempworkhour.Day = tempWorkHourDto.day;
            tempworkhour.Status = tempWorkHourDto.status;
            tempworkhour.Idemployee = tempWorkHourDto.idWorker;
            tempworkhour.Idroom = tempWorkHourDto.idroom;

            var isOk = await UpdateTempworkhour(id, tempworkhour);
            return isOk;
        }

        public async Task<bool> UpdateStatusTempworkhour(int id)
        {
            var tempworkhour = await GetTempworkhourById(id);
            if(tempworkhour == null)
            {
                return false;
            }
            tempworkhour.Status = true;
            var isOk = await UpdateTempworkhour(id, tempworkhour);
            return isOk;
        }

        public async Task<bool> UpdateTempworkhour(int id, Tempworkhour tempWorkHour)
        {


            _context.Entry(tempWorkHour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempworkhourExists(id))
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

        public async Task<List<TempWorkHourDto>> GetAllTempworkhours()
        {
            var list = new List<TempWorkHourDto>();
            var workhours = await _context.Tempworkhours.ToListAsync();
            foreach (var workhour in workhours)
            {
                if (workhour == null)
                {
                    continue;
                }
                var tempWorkHourDto = new TempWorkHourDto();
                tempWorkHourDto.id = workhour.Idtempworkhour;
                tempWorkHourDto.startHouer = workhour.Starthouer;
                tempWorkHourDto.endTime = workhour.Endtime;
                tempWorkHourDto.date = workhour.Date;
                tempWorkHourDto.day = workhour.Day;
                tempWorkHourDto.status = workhour.Status;
                tempWorkHourDto.idWorker = workhour.Idemployee;
                tempWorkHourDto.idroom = workhour.Idroom;
                tempWorkHourDto.nameRoom = await _iRoomsData.GetNameRoom(workhour.Idroom);
                list.Add(tempWorkHourDto);
            }
            return list;
        }
    }
}
