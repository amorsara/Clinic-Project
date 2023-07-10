using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string? NameEmployee { get; set; }

        public string? ColorEmployee { get; set; }

        public List<EmployeeShiftDto>? WeeklyHouers { get; set; }

    }
}
