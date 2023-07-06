using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class ForumXuser
{
    public int? FkUser { get; set; }

    public int? FkForum { get; set; }

    public int Id { get; set; }

    public virtual Forum FkForumNavigation { get; set; }

    public virtual DataUser FkUserNavigation { get; set; }
}
