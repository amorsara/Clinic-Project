using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class ShiftDto
    {
        public int idworkhour { get; set; }

        public int day { get; set; }

        public TimeOnly starthour { get; set;}
    }
}
