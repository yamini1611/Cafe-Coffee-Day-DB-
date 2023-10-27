using System;
using System.Collections.Generic;

namespace Cafe.Data.Models;

public partial class Role
{
    public int Roleid { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
