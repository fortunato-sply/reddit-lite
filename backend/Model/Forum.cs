using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Forum
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? Photo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Owner { get; set; }

    public virtual ICollection<ForumXuserRole> ForumXuserRoles { get; set; } = new List<ForumXuserRole>();

    public virtual DataUser? OwnerNavigation { get; set; }

    public virtual ImageDatum? PhotoNavigation { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<DataUser> FkUsers { get; set; } = new List<DataUser>();
}
