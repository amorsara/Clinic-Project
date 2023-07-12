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

        public string? nameWorker { get; set; }

        public string? colorWorker { get; set; }

        public List<EmployeeShiftDto>? weeklyHouers { get; set; }

    }
}
