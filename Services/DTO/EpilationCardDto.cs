using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class EpilationCardDto
    {
        public int idClient { get; set; }

        public string? remarkElec { get; set; }

        public List<EpilationtreatmentDto>? listTreatments { get; set; }

        public List<MedicalListDto>? MedicalList { get; set; }
    }
}
