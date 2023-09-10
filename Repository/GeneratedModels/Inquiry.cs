using System;
using System.Collections.Generic;

namespace Repository.GeneratedModels;

public partial class Inquiry
{
    public int Idinquirie { get; set; }

    public int Idemployee { get; set; }

    public bool? Doinquirie { get; set; }

    public DateOnly? Date { get; set; }

    public TimeOnly? Time { get; set; }

    public string? Fullname { get; set; }

    public string? Phon { get; set; }

    public string? Sum { get; set; }

    public int? Status { get; set; }

    public string? Remark { get; set; }

    public string? Response { get; set; }

    public virtual Employee IdemployeeNavigation { get; set; } = null!;
}
