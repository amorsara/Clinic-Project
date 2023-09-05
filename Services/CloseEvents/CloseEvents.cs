using Services.CloseRooms;
using Services.DTO;
using Services.TempCloseEmployees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CloseEvents
{
    public class CloseEvents : ICloseEvents
    {
        private readonly ICloseRoomsData _iCloseRoomsData;
        private readonly ITempCloseEmployeesData _iTempCloseEmployeesData;

        public CloseEvents(ICloseRoomsData closeRoomsData, ITempCloseEmployeesData tempCloseEmployeesData)
        {
            _iCloseRoomsData = closeRoomsData;
            _iTempCloseEmployeesData = tempCloseEmployeesData;
        }

        public async Task<List<CloseEventsDto>> GetAllCloseEvents()
        {
            var listEvents = await _iCloseRoomsData.GetCloseEventsForRooms();
            var closeEmployees = await _iTempCloseEmployeesData.GetCloseEventsForEmployees();
            listEvents.AddRange(closeEmployees);
            return listEvents;
        }
    }
}
