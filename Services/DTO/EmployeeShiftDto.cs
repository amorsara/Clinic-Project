using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class EmployeeShiftDto
    {
        public int IdEmployeeShift { get; set; }
        
        public TimeOnly? StartMorning { get; set; }

        public TimeOnly? EndMorning { get; set; }

        public TimeOnly? StartAfternoon { get; set; }

        public TimeOnly? EndAfternoon { get; set; }

        public TimeOnly? StartEvenning { get; set; }

        public TimeOnly? EndEvenning { get; set; }

    }
}
