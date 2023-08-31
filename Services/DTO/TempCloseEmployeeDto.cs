using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class TempCloseEmployeeDto
    {
        public int id { get; set; }

        public int idWorker { get; set; }

        public DateOnly? startDate { get; set; }

        public DateOnly? endDate { get; set; }

        public TimeOnly? startTime { get; set; }

        public TimeOnly? endTime { get; set; }

        public string? reason { get; set; }

        public bool? status { get; set; }
    }
}

