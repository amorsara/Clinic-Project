using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class AppointmentDto
    {
        public int idTreated { get; set; }

        public string? treatment { get; set; }

        public int idWorker { get; set; }

        public int idRoom { get; set; }

        public DateOnly? Date { get; set; }

        public TimeOnly? startHouer { get; set; }

        public TimeOnly? endTime { get; set; }

        public string? Remark { get; set; }

        public bool isRemined { get; set; }

        public bool? discount { get; set; }

    }
}


//{ idTreated: int, treatment:'l/e/a', area["11", "22"..],date: '2323-07-11',startHouer: 'hh:mm' ,
//        endTime: 'hh:mm' , Employee: "name" , Remark: "" , remined: bool , discount:bool}
