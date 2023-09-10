using System;
using System.Collections.Generic;

namespace Repository.GeneratedModels;

public partial class Closeroom
{
    public int Idcloseroom { get; set; }

    public DateOnly? Startdate { get; set; }

    public DateOnly? Enddate { get; set; }

    public TimeOnly? Starttime { get; set; }

    public TimeOnly? Endtime { get; set; }

    public string? Reason { get; set; }

    public string? Idrooms { get; set; }
}
