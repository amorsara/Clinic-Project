using System;
using System.Collections.Generic;

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

    public virtual Employee IdemployeeNavigation { get; set; } = null!;

    public virtual Room IdroomNavigation { get; set; } = null!;
}
