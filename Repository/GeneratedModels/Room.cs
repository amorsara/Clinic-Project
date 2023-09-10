using System;
using System.Collections.Generic;

namespace Repository.GeneratedModels;

public partial class Room
{
    public int Idroom { get; set; }

    public string? Nameroom { get; set; }

    public string? Treatmentstype { get; set; }

    public bool? Isshow { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Tempworkhour> Tempworkhours { get; set; } = new List<Tempworkhour>();

    public virtual ICollection<Workhour> Workhours { get; set; } = new List<Workhour>();
}
