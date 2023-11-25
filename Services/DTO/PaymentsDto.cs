using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class PaymentsDto
    {
        public int Id { get; set; }

        public int idContact { get; set; }

        public double allCredit { get; set; }

        public string? employee { get; set; }

        public int pay { get; set; }

        public int owes { get; set; }

        public int credit { get; set; }

        public string? datePayment { get; set; }

        public DateOnly date { get; set; }

        public List<string>? treatment { get; set; }

        public List<string>? laser { get; set; }

        public string? type { get; set; }

        public bool r { get; set; }

        public string? remark { get; set; }

        public List<string>? waxing { get; set; }

        public string? electrolysis { get; set;}

        public string? AdvancedElectrolysis { get; set;}

    }
}




