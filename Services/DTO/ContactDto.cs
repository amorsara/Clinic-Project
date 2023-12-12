using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class ContactDto
    {
        public int id { get; set; }

        public bool Sem { get; set; }

        public bool Active { get; set; }

        public int? Priority { get; set; }

        public bool? IsShow { get; set; }

        public List<ListFieldsDto?> Values { get; set; } = new List<ListFieldsDto?>();

        public int? credit { get; set; }
       


    }
}



