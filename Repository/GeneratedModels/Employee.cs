using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class Employee
{
    public int Idemployee { get; set; }

    public string? Name { get; set; }

    public string? Color { get; set; }

    public string? Password { get; set; }

    public bool? Permission { get; set; }

    public string? Treatmentstype { get; set; }

    public bool? Isshow { get; set; }

    [JsonIgnore]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [JsonIgnore]
    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    [JsonIgnore]
    public virtual ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();

    [JsonIgnore]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    [JsonIgnore]
    public virtual ICollection<Tempcloseemployee> Tempcloseemployees { get; set; } = new List<Tempcloseemployee>();

    [JsonIgnore]
    public virtual ICollection<Tempworkhour> Tempworkhours { get; set; } = new List<Tempworkhour>();

    [JsonIgnore]
    public virtual ICollection<Workhour> Workhours { get; set; } = new List<Workhour>();
}
