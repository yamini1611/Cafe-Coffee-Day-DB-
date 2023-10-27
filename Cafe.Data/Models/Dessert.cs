using System;
using System.Collections.Generic;

namespace Cafe.Data.Models;

public partial class Dessert
{
    public int Did { get; set; }

    public string? Dname { get; set; }

    public int? OriginalPrice { get; set; }

    public int? OfferPrice { get; set; }

    public int? Stock { get; set; }

    public byte[]? Image { get; set; }

    public string? Offer { get; set; }
}
