using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CloseEvents
{
    public interface ICloseEvents
    {
        Task<List<CloseEventsDto>> GetAllCloseEvents();
    }
}
