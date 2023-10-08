using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class MessageDto
    {
        public int id { get; set; }

        public EmployeeDetails? from { get; set; }

        public List<EmployeeDetails>? to { get; set; }

        public string? question { get; set; }

        public string? answer { get; set; }
    }
}

