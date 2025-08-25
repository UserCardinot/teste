using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEstruturaOrganizacional.Core.Entities
{
    [Table("Role", Schema = "TESTE")]
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Foreign keys
        public int SectorId { get; set; }

        // Navigation properties
        public virtual Sector Sector { get; set; } = null!;
        public virtual ICollection<JobPosition> JobPositions { get; set; } = new List<JobPosition>();

        public Role()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
