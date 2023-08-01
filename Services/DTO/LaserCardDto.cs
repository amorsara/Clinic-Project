using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class LaserCardDto
    {
        public int idClient { get; set; }

        public string? remarkLaser { get; set; }

        public List<LasertreatmentDto>? listTreatments { get; set;}
    }
}
