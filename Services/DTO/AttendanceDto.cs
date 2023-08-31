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


//{ employee: { id: 0,color: '',name: ''},date: 'yyyy-mm-dd',time: '00:00:00'}