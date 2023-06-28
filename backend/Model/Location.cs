using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Location
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int? Photo { get; set; }

    public virtual ImageDatum? PhotoNavigation { get; set; }
}
