﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class Room
{
    public int Idroom { get; set; }

    public string? Nameroom { get; set; }

    public string? Treatmentstype { get; set; }

    [JsonIgnore]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
