using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class LasertreatmentDto
    {
        public int Idlasertreatment { get; set; }

        public int idClient { get; set; }

        public string? colorWorker { get; set; }

        public DateOnly? Date { get; set; }

        public List<string>? Area { get; set; }

        public int? Ms { get; set; }

        public string? Spotsize { get; set; }

        public string? Energy { get; set; }

        public string? Results { get; set; }
    }
}
