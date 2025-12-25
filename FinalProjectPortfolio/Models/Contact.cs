using System.ComponentModel.DataAnnotations;

namespace FinalProjectPortfolio.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? GitHubUrl { get; set; }
        
        public string? Address { get; set; }
    }
}