using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CloseRooms
{
    public interface ICloseRoomsData
    {
        Task<List<Closeroom>> GetCloserooms();

        Task<List<CloseRoomDto>> GetAllCloserooms();

        Task<List<CloseEventsDto>> GetCloseEventsForRoomsForWeek(DateOnly date);

        Task<bool> CancelAppointment(DateOnly date);

        Task<Closeroom?> GetCloseroomById(int id);

        Task<bool> UpdateCloseroom(int id, Closeroom closeroom);

        Task<bool> CreateCloseroom(CloseRoomDto closeRoomDto);

        Task<bool> DeleteCloseroom(int id);

        bool CloseroomExists(int id);
    }
}
