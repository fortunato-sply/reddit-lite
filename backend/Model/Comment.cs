using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Comment
{
    public int Id { get; set; }

    public string Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? FkPost { get; set; }

    public int? FkUser { get; set; }

    public virtual Post FkPostNavigation { get; set; }

    public virtual DataUser FkUserNavigation { get; set; }
}
