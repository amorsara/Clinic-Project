using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class ScheduleDto
    {
        public int Id { get; set; }

        public int? IdContact { get; set; }

        public int idWorker { get; set; }

        public DateOnly? Date { get; set; }

        public TimeOnly? startHouer { get; set; }

        public TimeOnly? endTime { get; set; }

        public string? colorWorker { get; set; }

        public string? nameRoom { get; set; }

        public string? firstName { get; set; }

        public string? lastName { get; set; }

        public int? isRemined { get; set; }

        public string? note { get; set; }

        public string? type { get; set; }

        public string? phone1 { get; set; }

        public string? phonen2 { get; set; }

        public string? phone3 { get; set; }

        public char? shift { get; set; }

        public bool? cancel { get; set; }

        public string? detailsType { get; set; }

        public bool isPay { get; set; }
    }
}
