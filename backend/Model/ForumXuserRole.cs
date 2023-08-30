using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class ForumXuserRole
{
    public int? FkUser { get; set; }

    public int? FkRole { get; set; }

    public int? FkForum { get; set; }

    public int Id { get; set; }

    public virtual Forum FkForumNavigation { get; set; }

    public virtual Role FkRoleNavigation { get; set; }

    public virtual DataUser FkUserNavigation { get; set; }
}
