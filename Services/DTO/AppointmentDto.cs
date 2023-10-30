using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class AppointmentDto
    {
        public int idTreated { get; set; }

        public string? treatment { get; set; }

        public int idWorker { get; set; }

        public int idRoom { get; set; }

        public DateOnly? Date { get; set; }

        public TimeOnly? startHouer { get; set; }

        public TimeOnly? endTime { get; set; }

        public int duration { get; set; }

        public List<string>? area { get; set; }

        public string? Remark { get; set; }

        public bool isRemined { get; set; }

        public bool? discount { get; set; }

        public int contactId { get; set; }
        
        public ContactDto contact { get; set; }

    }
}
