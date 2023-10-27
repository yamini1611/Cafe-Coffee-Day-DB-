using System.ComponentModel.DataAnnotations;
#nullable disable
namespace Cafe.Models
{
    public class EmailRequest
    {
        [Required]
        public string Product { get; set; }
        [Required]

        public string ToEmail { get; set; }
        [Required]

        public string Uname { get; set; }
        [Required]

        public int TotalPrice { get; set; }
    
    }
}
