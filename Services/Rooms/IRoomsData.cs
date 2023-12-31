﻿using Repository.GeneratedModels;
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

        Task<List<RoomDetails>> GetAllNamesWithIdRooms();

        Task<bool> ChangeRooms(List<List<RoomEmployeeDto>> rooms);

        Task<List<List<RoomEmployeeDto>>> GetAllRoomsWithTypes();

        Task<Room?> GetRoomById(int id);

        Task<bool> DeleteRoomById(int id);

        Task<bool> CreateRoom(Room room);

        Task<string?> GetNameRoom(int id);

        Task<Room?> GetRoomByName(string? name);

        Task<int> GetRoomIdByName(string? name);

        Task<List<string>> GetAllNameRooms();

        Task<List<string>?> GetTreatmentsForRoom(int id); 

        Task<List<Employee>> GetAllEmployeesForRoom(int id);

        Task<List<RoomFieldsDto>> GetAllFieldsForRoom();

        Task<List<int>> GetAllRoomsIdForEmployee(int id);

        Task<bool> UpdateRoom(int id, Room room);

        bool RoomExists(int id);
    }
}
