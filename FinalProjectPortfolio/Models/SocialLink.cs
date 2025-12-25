using System.ComponentModel.DataAnnotations;

namespace FinalProjectPortfolio.Models
{
    public class SocialLink
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Url { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string IconClass { get; set; } = "bi-link-45deg"; // Default icon

        [StringLength(50)]
        public string? Name { get; set; } // e.g., "LinkedIn", "GitHub" for accessibility
    }
}