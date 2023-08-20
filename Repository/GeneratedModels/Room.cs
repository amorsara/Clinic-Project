using System;
using System.Collections.Generic;

namespace Repository.GeneratedModels;

public partial class Room
{
    public int Idroom { get; set; }

    public string? Nameroom { get; set; }

    public string? Treatmentstype { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Workhour> Workhours { get; set; } = new List<Workhour>();
}
