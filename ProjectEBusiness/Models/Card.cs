using Microsoft.Build.Framework;

namespace ProjectEBusiness.Models
{
    public class Card
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
