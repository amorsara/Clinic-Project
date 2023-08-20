using System;
using System.Collections.Generic;

namespace Repository.GeneratedModels;

public partial class Payment
{
    public int Idpayment { get; set; }

    public int Idcontact { get; set; }

    public DateOnly? Date { get; set; }

    public string? Datepayment { get; set; }

    public string? Treatment { get; set; }

    public string? Area { get; set; }

    public string? Type { get; set; }

    public int? Pay { get; set; }

    public int? Owes { get; set; }

    public int? Credit { get; set; }

    public bool? R { get; set; }

    public string? Employee { get; set; }

    public virtual Contact IdcontactNavigation { get; set; } = null!;
}
