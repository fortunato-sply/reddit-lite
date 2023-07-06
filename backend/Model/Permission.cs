using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Permission
{
    public int Id { get; set; }

    public int? Tier { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Role> FkRoles { get; set; } = new List<Role>();
}
