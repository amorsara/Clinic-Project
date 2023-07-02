using System;
using System.Collections.Generic;

namespace Repository.GeneratedModels;

public partial class Employee
{
    public int Idemployee { get; set; }

    public string? Name { get; set; }

    public string? Color { get; set; }

    public string? Password { get; set; }

    public bool? Permission { get; set; }

    public bool? Laser { get; set; }

    public bool? Electrolysis { get; set; }

    public bool? Waxing { get; set; }

    public bool? Advancedelectrolysis { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Workhour> Workhours { get; set; } = new List<Workhour>();
}
