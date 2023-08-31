using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class TempWorkHourDto
    {
        public int id { get; set; }

        public int idWorker { get; set; }

        public int idroom { get; set; }

        public DateOnly? date { get; set; }

        public int? day { get; set; }

        public TimeOnly? endTime { get; set; }

        public TimeOnly? startHouer { get; set; }

        public bool? status { get; set; }
    }
}

