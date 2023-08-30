using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class Tempcloseemployee
{
    public int Idtempcloseemployee { get; set; }

    public int Idemployee { get; set; }

    public DateOnly? Startdate { get; set; }

    public DateOnly? Enddate { get; set; }

    public TimeOnly? Starttime { get; set; }

    public TimeOnly? Endtime { get; set; }

    public string? Reason { get; set; }

    [JsonIgnore]
    public virtual Employee IdemployeeNavigation { get; set; } = null!;
}
