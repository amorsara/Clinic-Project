using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class AccountsDto
    {
        public int id { get; set; }

        public string? datePayment {get; set;}

        public DateOnly? date { get; set;}

        public string? fullName { get; set;}    

        public string? phone { get; set;}

        public List<string>? tretment { get; set;}

        public List<string>? laser { get; set; }

        public List<string>? waxing { get; set; }

        public string? electrolysis { get; set; }

        public string? AdvancedElectrolysis { get; set; }

        public string? employee { get; set;}

        public double? Payed { get; set;}

        public string? type { get; set;}

        public double? Debt { get; set;}

        public double? credit { get; set;}

        public double? allCredit { get; set; }

        public string? remark { get; set;}  

        public bool? r { get; set; }
    }
}




