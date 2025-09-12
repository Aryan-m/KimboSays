using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataRepo.Models
{
    public class KimboTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "TEXT")]
        public string Task { get; set; } = string.Empty;

        [MaxLength(400)]
        [Column(TypeName = "TEXT")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int EffortId { get; set; }
        public virtual TaskEffort? Effort { get; set; }

        [Column(TypeName = "DATE")]
        public DateOnly DateAdded { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
