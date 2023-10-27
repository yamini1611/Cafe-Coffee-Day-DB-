using System;
using System.Collections.Generic;

namespace Cafe.Data.Models;

public partial class Checkout
{
    public int Chid { get; set; }

    public string? Product { get; set; }

    public int? TotalPrice { get; set; }

    public int? Userid { get; set; }

    public virtual User? User { get; set; }
}
