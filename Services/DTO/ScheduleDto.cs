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

        public DateOnly? Date { get; set; }

        public TimeOnly? StartHouer { get; set; }

        public TimeOnly? EndHouer { get; set; }

        public string? ColorEmployee { get; set; }

        public int IdEmployee { get; set; }

        public string? NameRoom { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? IsRemined { get; set; }

        public string? Remark { get; set; }

        public string? Type { get; set; }

        public string? Phonenumber1 { get; set; }

        public string? Phonenumber2 { get; set; }

        public string? Phonenumber3 { get; set; }

        public int Shift { get; set; }

        public bool? Cancel { get; set; }
    }
}
