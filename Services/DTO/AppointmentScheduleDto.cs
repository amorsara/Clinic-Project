using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class AppointmentScheduleDto
    {
        public int Idappointment { get; set; }
        public int? Idcontact { get; set; }
        public TimeOnly? Timestart { get; set; }
        public TimeOnly? Timeend { get; set; }
        public DateOnly? Date { get; set; }
        public string Treatmentname { get; set; }
        public int? Isremaind { get; set; }
        public bool? Cancle { get; set; }
        public int Idemployee { get; set; }
        public string Color { get; set; }
        public string RoomName { get; set; }
        public char? Shift { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Remark { get; set; }
        public string Phonenumber1 { get; set; }
        public string Phonenumber2 { get; set; }
        public string Phonenumber3 { get; set; }
        public int Duration { get; set; }
        public string Area { get; set; }
        public bool Ispay { get; set; }
        public bool Discount { get; set; }

    }
}