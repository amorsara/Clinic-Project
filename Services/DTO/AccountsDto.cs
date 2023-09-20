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

        public string? Advanced { get; set; }

        public string? employee { get; set;}

        public int? Payed { get; set;}

        public string? type { get; set;}

        public int? Debt { get; set;}

        public int? credit { get; set;}

        public int? allCredit { get; set; }

        public string? remark { get; set;}  
    }
}




 //{ "id": 153, "datePayment": "2023-09-06 15:59", "date": "2023-09-06", "fullName": "Itty Padwa-Pinter",
 //"phone": "0533149034", "tretment": [ "Electrolysis" ], "laser": [ "" ], "waxing": [ "" ], "electrolysis": "",
 //"advancedElectrolysis": "", "employee": "purple", "payed": 60, "type": "Cash", "debt": 10, "credit": 0, "allCredit": null, "remark": null }




