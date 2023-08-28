using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Repository.GeneratedModels;

public partial class Message
{
    public int Idmessage { get; set; }

    public int Idfrom { get; set; }

    public string? Idto { get; set; }

    public string? Question { get; set; }

    public string? Answer { get; set; }

    [JsonIgnore]
    public virtual Employee IdfromNavigation { get; set; } = null!;
}
