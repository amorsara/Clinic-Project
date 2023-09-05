using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class FutureDateDto
    {
        public DateOnly? date { get; set; }

        public TimeOnly? startHour { get; set; }

        public TimeOnly? endTime { get; set; }

        public string? treatment { get; set; }
    }
}


