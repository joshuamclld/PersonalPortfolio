using System;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectPortfolio.Models
{
    public class Education
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Institution { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Degree { get; set; } = string.Empty;

        [StringLength(100)]
        public string? FieldOfStudy { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; } // Nullable for ongoing education

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
