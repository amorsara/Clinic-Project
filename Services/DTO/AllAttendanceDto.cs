using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class AllAttendanceDto
    {
        public int id { get; set; }

        public EmployeeDetails? employee { get; set; }

        public DateOnly? date { get; set; }

        public TimeOnly? timeEnter { get; set; }

        public TimeOnly? timeExit { get; set; }

        public TimeSpan? sum { get; set; }
    }
}
