using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Like
{
    public int Id { get; set; }

    public int Value { get; set; }

    public int? FkUser { get; set; }

    public int? FkPost { get; set; }

    public virtual Post? FkPostNavigation { get; set; }

    public virtual DataUser? FkUserNavigation { get; set; }
}
