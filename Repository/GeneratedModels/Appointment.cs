using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class Appointment
{
    public int Idappointment { get; set; }

    public char? Treatmentname { get; set; }

    public int Idemployee { get; set; }

    public int Idcontact { get; set; }

    public int Idroom { get; set; }

    public DateOnly? Date { get; set; }

    public TimeOnly? Duration { get; set; }

    public TimeOnly? Timestart { get; set; }

    public TimeOnly? Timeend { get; set; }

    public string? Remark { get; set; }

    public int? Isremaind { get; set; }

    public bool? Discount { get; set; }

    public bool? Cancle { get; set; }

    public bool? Wait { get; set; }

    [JsonIgnore]
    public virtual Contact IdcontactNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Employee IdemployeeNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Room IdroomNavigation { get; set; } = null!;
}
