using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class ContactDateDto
    {
        public int Idcontact { get; set; }

        public bool[] Treatment { get; set; } = new bool[3];

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string? Phonenumber1 { get; set; }

        public string? Phonenumber2 { get; set; }

        public string? Phonenumber3 { get; set; }

        public string? Email { get; set; }

        public string? Sem { get; set; }  

        public string? Remark { get; set; }

        public bool? Isactive { get; set; }

        public int? allCredit { get; set; }

        public bool? isshow { get; set; }

        public List<DateOnly>? ListDates { get; set; }
    }
}
