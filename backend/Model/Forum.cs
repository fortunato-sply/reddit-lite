using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Forum
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int? Photo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Owner { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<ForumXuserRole> ForumXuserRoles { get; set; } = new List<ForumXuserRole>();

    public virtual ICollection<ForumXuser> ForumXusers { get; set; } = new List<ForumXuser>();

    public virtual DataUser OwnerNavigation { get; set; }

    public virtual ImageDatum PhotoNavigation { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
