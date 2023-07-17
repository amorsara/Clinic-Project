using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class Room
{
    public int Idroom { get; set; }

    public string? Nameroom { get; set; }

    public bool? Laser { get; set; }

    public bool? Electrolysis { get; set; }

    public bool? Waxing { get; set; }

    public bool? Advancedelectrolysis { get; set; }

    [JsonIgnore]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
