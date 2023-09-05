using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class CloseEventsDto
    {
        public int id { get; set; }

        public string? nameEvent { get; set; }

        public int idRoom { get; set; }

        public DateOnly? date { get; set; }

        public TimeOnly? startHour { get; set; }

        public TimeOnly? endTime { get; set; }

        public EmployeeDetails? employee { get; set; }

    }
}
