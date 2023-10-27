using System;
using System.Collections.Generic;

namespace Cafe.Data.Models;

public partial class User
{
    public int Userid { get; set; }

    public string? Uname { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? Roleid { get; set; } = 2;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();

    public virtual Role? Role { get; set; }
}
