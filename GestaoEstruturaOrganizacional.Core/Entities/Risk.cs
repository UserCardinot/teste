using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEstruturaOrganizacional.Core.Entities
{
    [Table("Risk", Schema = "TESTE")]
    public class Risk
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(100)]
        public string? Category { get; set; }

        [StringLength(50)]
        public string? Severity { get; set; }

        [StringLength(1000)]
        public string? ControlMeasures { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Foreign keys
        public int JobPositionId { get; set; }

        // Navigation properties
        public virtual JobPosition JobPosition { get; set; } = null!;

        public Risk()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
