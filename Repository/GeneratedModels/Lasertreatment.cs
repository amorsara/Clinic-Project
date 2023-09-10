using System;
using System.Collections.Generic;

namespace Repository.GeneratedModels;

public partial class Lasertreatment
{
    public int Idlasertreatment { get; set; }

    public int Idcontact { get; set; }

    public string? Coloremployee { get; set; }

    public DateOnly? Date { get; set; }

    public string? Area { get; set; }

    public string? Ms { get; set; }

    public string? Spotsize { get; set; }

    public string? Energy { get; set; }

    public string? Results { get; set; }

    public virtual Contact IdcontactNavigation { get; set; } = null!;
}
