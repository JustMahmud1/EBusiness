
using System.ComponentModel.DataAnnotations;

namespace ProjectEBusiness.DTOs.CardDTOs
{
    public class CardPostDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
