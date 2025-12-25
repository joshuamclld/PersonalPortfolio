using System.ComponentModel.DataAnnotations;

namespace FinalProjectPortfolio.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public string? ProjectLink { get; set; }
    }
}