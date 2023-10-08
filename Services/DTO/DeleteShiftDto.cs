using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public  class DeleteShiftDto
    {
        public int idWorker { get; set; }

        public int day { get; set; }

        public TimeOnly start { get; set; }
    }
}

