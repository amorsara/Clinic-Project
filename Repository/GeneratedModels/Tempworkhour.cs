using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class Tempworkhour
{
    public int Idtempworkhour { get; set; }

    public int Idroom { get; set; }

    public int Idemployee { get; set; }

    public int? Day { get; set; }

    public TimeOnly? Starthouer { get; set; }

    public TimeOnly? Endtime { get; set; }

    public bool? Status { get; set; }

    public DateOnly? Date { get; set; }
    [JsonIgnore]

    public virtual Employee IdemployeeNavigation { get; set; } = null!;
    [JsonIgnore]

    public virtual Room IdroomNavigation { get; set; } = null!;
}
