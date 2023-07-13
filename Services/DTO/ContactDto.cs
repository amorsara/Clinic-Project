using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class ContactDto
    {
        public bool Sem { get; set; }

        public bool Active { get; set; }

        public string? Priority { get; set; }

        public List<ListFieldsDto?> Values { get; set; } = new List<ListFieldsDto?>();
    }
}

