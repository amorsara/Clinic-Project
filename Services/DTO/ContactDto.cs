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

//{
//    "sem": true,
//  "active": true,
//  "priority": "Phonenumber2",
//  "values": [
//    {
//        "field1": "kjhgfds",
//      "field2": "058956548",
//      "field3": "tammar"
//    },
//  {
//        "field1": "come",
//      "field2": "852545856",
//      "field3": "family"
//    },  {
//        "field1": "form",
//      "field2": "852852",
//      "field3": "t@gmail.com"
//    }
//  ]
//}

