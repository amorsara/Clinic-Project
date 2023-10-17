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

        public HairDto? hair { get; set; }

        public string? skin { get; set; }

        public List<LasertreatmentDto>? listTreatments { get; set;}

        public List<MedicalListDto>? MedicalList { get; set; }
    }
}



