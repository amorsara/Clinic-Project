using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class RoomScheduleDto
    {
        public string? nameRoom { get; set; } // add list type of treatment  - update get object i need find id..

        public List<EmployeeDto>? Employees { get; set; }
    }
}
