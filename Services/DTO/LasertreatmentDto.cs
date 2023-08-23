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

        public List<string>? Ms { get; set; }

        public List<string>? Spotsize { get; set; }

        public List<string>? Energy { get; set; }

        public List<string>? Results { get; set; }

        public string? remarkLaser { get; set; }
    }
}
