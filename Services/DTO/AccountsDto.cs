﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class AccountsDto
    {
        public string? datePayment {get; set;}

        public DateOnly? date { get; set;}

        public string? fullName { get; set;}    

        public string? phone { get; set;}

        public List<string>? tretment { get; set;}

        public string? employee { get; set;}

        public int? Payed { get; set;}

        public string? type { get; set;}

        public int? Debt { get; set;}

        public int? credit { get; set;}
    }
}




//{ datePayment: 'yy-mm-dd-hh-mm', date: 'yyyy-mm-dd', fullName: '', phon: '', tretment: ['','']
//, employee: '', payed: '', type: '', debt: '', credit: ''}