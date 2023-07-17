using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class WaitTreatmentsDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Phonenumber1 { get; set; }
        public string? Phonenumber2 { get; set; }
        public char? Type { get; set; } 
        public DateOnly Date { get; set; }
        public string? Remark { get; set; }
    }
}
