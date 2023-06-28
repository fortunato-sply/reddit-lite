using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class ForumXuserRole
{
    public int FkUser { get; set; }

    public int FkRole { get; set; }

    public int FkForum { get; set; }

    public virtual Forum FkForumNavigation { get; set; } = null!;

    public virtual Role FkRoleNavigation { get; set; } = null!;

    public virtual DataUser FkUserNavigation { get; set; } = null!;
}
