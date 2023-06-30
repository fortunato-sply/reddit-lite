using System;
using System.Collections.Generic;

namespace backend.ModelDTO;

public partial class DataUser
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime Born { get; set; }

    public int? Photo { get; set; }
}
