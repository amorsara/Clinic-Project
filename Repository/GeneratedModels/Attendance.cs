using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class Attendance
{
    public int Idattendance { get; set; }

    public int Idemployee { get; set; }

    public DateOnly? Date { get; set; }

    public TimeOnly? Timeenter { get; set; }

    public TimeOnly? Timeexit { get; set; }

    public bool? R { get; set; }

    [JsonIgnore]
    public virtual Employee IdemployeeNavigation { get; set; } = null!;
}
