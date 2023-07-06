using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class ImageDatum
{
    public int Id { get; set; }

    public byte[] Photo { get; set; }

    public virtual ICollection<DataUser> DataUsers { get; set; } = new List<DataUser>();

    public virtual ICollection<Forum> Forums { get; set; } = new List<Forum>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
