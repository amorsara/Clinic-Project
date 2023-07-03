using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class Contact
{
    public int Idcontact { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Mobilephone { get; set; }

    public string? Homephone { get; set; }

    public string? Numberphone { get; set; }

    public string? Email { get; set; }

    public bool? Sem { get; set; }

    public string? Remark { get; set; }

    public string? Howcomeus { get; set; }

    public bool? Active { get; set; }

    public string? Url { get; set; }

    [JsonIgnore]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
