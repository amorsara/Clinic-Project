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
        public int allCredit { get; set; }
        public int pay { get; set; }
        public int owes { get; set; }
        public int credit { get; set; }
        public DateTime datePayment { get; set;}
        public DateOnly date { get; set; }
        public List<string>? treatment { get; set; }
        public List<string>? area { get; set; }
        public string? type { get; set; }
        public bool r { get; set; }

    }
}


//{ allCredit: 0,pay: 0,owes: 0,credit: 0,idContact: 0
//,datePayment: 'yy-mm-dd-hh-mm', date: 'yyyy-mm-dd',treatment: [],area: [],type: '',r: true}
