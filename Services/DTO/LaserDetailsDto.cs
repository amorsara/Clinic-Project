using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class LaserDetailsDto
    {
        public int idTreated { get; set; }

        public string? remarkLaser { get; set; }

        public HairDto? hair { get; set; }   

        public string? skin { get; set; }
    }
}
