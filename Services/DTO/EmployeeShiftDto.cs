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
        
        public TimeOnly? startMorning { get; set; }

        public TimeOnly? endMorning { get; set; }

        public TimeOnly? startAfternoon { get; set; }

        public TimeOnly? endAfternoon { get; set; }

        public TimeOnly? startEvenning { get; set; }

        public TimeOnly? endEvenning { get; set; }

    }
}
