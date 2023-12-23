using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class Epilationtreatment
{
    public int Idepilationtreatment { get; set; }

    public int Idcontact { get; set; }

    public string? Coloremployee { get; set; }

    public DateOnly? Date { get; set; }

    public string? Area { get; set; }

    public string? Machine { get; set; }

    public string? Techniqe { get; set; }

    public string? Results { get; set; }

    public string? Probe { get; set; }

    public string? Parameters { get; set; }

    public string? Time { get; set; }
    [JsonIgnore]
    public virtual Contact IdcontactNavigation { get; set; } = null!;
}
