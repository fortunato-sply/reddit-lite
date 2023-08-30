using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<ForumXuserRole> ForumXuserRoles { get; set; } = new List<ForumXuserRole>();

    public virtual ICollection<RoleXpermission> RoleXpermissions { get; set; } = new List<RoleXpermission>();
}
