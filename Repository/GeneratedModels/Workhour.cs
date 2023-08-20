﻿using System;
using System.Collections.Generic;

namespace Repository.GeneratedModels;

public partial class Workhour
{
    public int Idworkhour { get; set; }

    public int Idemployee { get; set; }

    public int? Day { get; set; }

    public char? Shift { get; set; }

    public TimeOnly? Starthour { get; set; }

    public TimeOnly? Endhour { get; set; }

    public bool? Regularwork { get; set; }

    public int? Idroom { get; set; }

    public virtual Employee IdemployeeNavigation { get; set; } = null!;

    public virtual Room? IdroomNavigation { get; set; }
}
