using System;
using System.Collections.Generic;

namespace Repository.GeneratedModels;

public partial class Appointment
{
    public int Idappointment { get; set; }

    public string? Treatmentname { get; set; }

    public int Idemployee { get; set; }

    public int Idcontact { get; set; }

    public DateOnly? Date { get; set; }

    public TimeOnly? Duration { get; set; }

    public TimeOnly? Timestart { get; set; }

    public TimeOnly? Timeend { get; set; }

    public string? Remark { get; set; }

    public bool? Remainder { get; set; }

    public bool? Discount { get; set; }

    public bool? Cancle { get; set; }

    public bool? Wait { get; set; }

    public virtual Contact IdcontactNavigation { get; set; } = null!;

    public virtual Employee IdemployeeNavigation { get; set; } = null!;
}
