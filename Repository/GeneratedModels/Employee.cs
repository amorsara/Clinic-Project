using System;
using System.Collections.Generic;

namespace Repository.GeneratedModels;

public partial class Employee
{
    public int Idemployee { get; set; }

    public string? Name { get; set; }

    public string? Color { get; set; }

    public string? Password1 { get; set; }

    public string? Password2 { get; set; }

    public string? Treatmentstype { get; set; }

    public bool? Isshow { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Tempcloseemployee> Tempcloseemployees { get; set; } = new List<Tempcloseemployee>();

    public virtual ICollection<Tempworkhour> Tempworkhours { get; set; } = new List<Tempworkhour>();

    public virtual ICollection<Workhour> Workhours { get; set; } = new List<Workhour>();
}
