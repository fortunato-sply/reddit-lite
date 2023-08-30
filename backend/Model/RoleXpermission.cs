using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class RoleXpermission
{
    public int? FkRole { get; set; }

    public int? FkPermission { get; set; }

    public int Id { get; set; }

    public virtual Permission FkPermissionNavigation { get; set; }

    public virtual Role FkRoleNavigation { get; set; }
}
