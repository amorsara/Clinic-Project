using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class RoomScheduleDto
    {
        public string? NameRoom { get; set; }

        public List<EmployeeDto>? Employees { get; set; }
    }
}
