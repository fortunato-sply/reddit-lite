using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Post
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? FkUser { get; set; }

    public int? FkForum { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Forum? FkForumNavigation { get; set; }

    public virtual DataUser? FkUserNavigation { get; set; }

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
}
