using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEstruturaOrganizacional.Core.Entities
{
    [Table("EPI", Schema = "TESTE")]
    public class EPI
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Type { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public int? ReplacementPeriod { get; set; }

        [StringLength(50)]
        public string? CANumber { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Foreign keys
        public int JobPositionId { get; set; }

        // Navigation properties
        public virtual JobPosition JobPosition { get; set; } = null!;

        public EPI()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
