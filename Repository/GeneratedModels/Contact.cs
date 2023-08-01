using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class Contact
{
    public int Idcontact { get; set; }

    public bool? Laser { get; set; }

    public bool? Waxing { get; set; }

    public bool? Electrolysis { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Phonenumber1 { get; set; }

    public string? Phonenumber2 { get; set; }

    public string? Phonenumber3 { get; set; }

    public string? Email { get; set; }

    public bool? Sem { get; set; }

    public string? Remark { get; set; }

    public string? Howcomeus { get; set; }

    public bool? Isactive { get; set; }

    public string? Urlfile { get; set; }

    [JsonIgnore]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [JsonIgnore]
    public virtual ICollection<Epilationtreatment> Epilationtreatments { get; set; } = new List<Epilationtreatment>();

    [JsonIgnore]
    public virtual ICollection<Lasertreatment> Lasertreatments { get; set; } = new List<Lasertreatment>();
}
