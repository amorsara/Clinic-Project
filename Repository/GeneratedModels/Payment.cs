﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class Payment
{
    public int Idpayment { get; set; }

    public int Idcontact { get; set; }

    public DateOnly? Date { get; set; }

    public string? Datepayment { get; set; }

    public string? Treatment { get; set; }

    public string? Laser { get; set; }

    public string? Type { get; set; }

    public double? Pay { get; set; }

    public double? Owes { get; set; }

    public double? Credit { get; set; }

    public bool? R { get; set; }

    public string? Employee { get; set; }

    public string? Remark { get; set; }

    public string? Electrolysis { get; set; }

    public string? Waxing { get; set; }

    public string? Advanced { get; set; }
    [JsonIgnore]
    public virtual Contact IdcontactNavigation { get; set; } = null!;
}
