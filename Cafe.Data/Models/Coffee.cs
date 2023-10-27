using System.ComponentModel.DataAnnotations;

namespace Cafe.Data.Models
{
    public class Coffee
    {
        public int? Coffeeid { get; set; }

        [Required(ErrorMessage = "CoffeeName is required.")]
        public string? CoffeeName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "OriginalPrice should be greater than zero.")]
        public int OriginalPrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "OfferPrice should be greater than zero.")]
        public int OfferPrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock should be a non-negative value.")]
        public int Stock { get; set; }

        public byte[]? Image { get; set; }

        public string? Offer { get; set; }
    }
}