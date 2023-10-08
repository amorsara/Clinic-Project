using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class AttendanceDto
    {
        public int id { get; set; }

        public EmployeeDetails? employee { get; set; }

        public DateOnly date { get; set; }

        public TimeOnly time { get;  set; }
    }
}

