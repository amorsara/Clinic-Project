﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class Job
{
    public long Id { get; set; }

    public long? Stateid { get; set; }

    public string? Statename { get; set; }

    public string Invocationdata { get; set; } = null!;

    public string Arguments { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public DateTime? Expireat { get; set; }

    public int Updatecount { get; set; }
    [JsonIgnore]

    public virtual ICollection<Jobparameter> Jobparameters { get; set; } = new List<Jobparameter>();
    [JsonIgnore]

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
