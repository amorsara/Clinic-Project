using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class RoomFieldsDto
    {
        public int Id { get; set; }

        public int IdRoom { get; set; }

        public string? nameRoom { get; set; }

        public List<string>? fields { get; set; }
    }
}
