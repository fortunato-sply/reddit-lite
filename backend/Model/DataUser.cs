﻿using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class DataUser
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Salt { get; set; }

    public DateTime Born { get; set; }

    public int? Photo { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<ForumXuserRole> ForumXuserRoles { get; set; } = new List<ForumXuserRole>();

    public virtual ICollection<ForumXuser> ForumXusers { get; set; } = new List<ForumXuser>();

    public virtual ICollection<Forum> Forums { get; set; } = new List<Forum>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ImageDatum PhotoNavigation { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
