using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class Waiting
{
    public int Idwaiting { get; set; }

    public string? Fullname { get; set; }

    public string? Phone1 { get; set; }

    public string? Phone2 { get; set; }

    public string? Remark { get; set; }

    public DateOnly? Untildate { get; set; }

    public char? Type { get; set; }
}
