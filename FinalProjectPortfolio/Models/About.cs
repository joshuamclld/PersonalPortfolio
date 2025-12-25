using System.ComponentModel.DataAnnotations;

namespace FinalProjectPortfolio.Models
{
    public class About
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;
    }
}