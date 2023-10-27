using System;
using System.Collections.Generic;

namespace Cafe.Data.Models;

public partial class MilkShake
{
    public int Mid { get; set; }

    public string? Mname { get; set; }

    public int? OriginalPrice { get; set; }

    public int? OfferPrice { get; set; }

    public int? Stock { get; set; }

    public byte[]? Image { get; set; }

    public string? Offer { get; set; }
}
