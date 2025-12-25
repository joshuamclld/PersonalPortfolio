using System.ComponentModel.DataAnnotations;

namespace FinalProjectPortfolio.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string ShortBio { get; set; } = string.Empty;

        public string? ProfilePictureUrl { get; set; }
        
        public string? CVUrl { get; set; }
    }
}