using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class EpilationtreatmentDto
    {
        public int Idepilationtreatment { get; set; }

        public int idClient { get; set; }

        public string? colorWorker { get; set; }

        public DateOnly? Date { get; set; }

        public List<string>? Area { get; set; }

        public string? Machine { get; set; }

        public int? Time { get; set; }

        public string? Techniqe { get; set; }

        public string? Results { get; set; }

        public string? remarkElecr { get; set; }
    }
}
