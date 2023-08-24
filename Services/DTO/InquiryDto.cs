using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class InquiryDto
    {
        public int id { get; set; }

        public bool? doInquirie { get; set; }

        public DateOnly? date { get; set; }

        public EmployeeDetails? employee { get; set; }

        public TimeOnly? time { get; set; }

        public string? fullname { get; set; }

        public string? phon { get; set; }

        public string? sum { get; set; }

        public int? status { get; set; }

        public string? remark { get; set; }

        public string? response { get; set; }
    }
}


