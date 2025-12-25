using System.ComponentModel.DataAnnotations;

namespace FinalProjectPortfolio.Models
{
    public class Experience
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string JobTitle { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Company { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}