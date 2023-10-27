using System;
using System.Collections.Generic;

namespace Cafe.Data.Models;

public partial class Cart
{
    public int Cid { get; set; }

    public string? Product { get; set; }

    public int? Price { get; set; }

    public int? Quantity { get; set; }

    public string? Image { get; set; }

    public int? Userid { get; set; }

    public virtual User? User { get; set; }
}
