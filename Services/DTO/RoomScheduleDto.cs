using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class RoomScheduleDto
    {
        public int IdRoom { get; set; }

        public string? nameRoom { get; set; }

        public List<string?>? listTreatments { get; set; }

        public List<EmployeeDto>? Employees { get; set; }
    }
}
