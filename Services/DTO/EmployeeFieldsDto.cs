using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class EmployeeFieldsDto
    {
        public int Id { get; set; }

        public int IdWorker { get; set; }

        public string? nameWorker { get; set; }

        public string? colorWorker { get; set; }

        public List<string>? fields { get; set; }
    }
}
