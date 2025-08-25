using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEstruturaOrganizacional.Core.Entities
{
    [Table("JobPosition", Schema = "TESTE")]
    public class JobPosition
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [StringLength(20)]
        public string? CBOCode { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(100)]
        public string? EducationLevel { get; set; }

        [StringLength(255)]
        public string? Experience { get; set; }

        [StringLength(1000)]
        public string? Skills { get; set; }

        [StringLength(1000)]
        public string? Competencies { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Foreign keys
        public int RoleId { get; set; }
        public int? ReportingToId { get; set; }

        // Navigation properties
        public virtual Role Role { get; set; } = null!;
        public virtual JobPosition? ReportingTo { get; set; }
        public virtual ICollection<JobPosition> Subordinates { get; set; } = new List<JobPosition>();
        public virtual ICollection<EPI> EPIs { get; set; } = new List<EPI>();
        public virtual ICollection<Risk> Risks { get; set; } = new List<Risk>();

        public JobPosition()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        // Helper methods
        public void AddEPI(EPI epi)
        {
            EPIs.Add(epi);
            epi.JobPosition = this;
        }

        public void RemoveEPI(EPI epi)
        {
            EPIs.Remove(epi);
            epi.JobPosition = null;
        }

        public void AddRisk(Risk risk)
        {
            Risks.Add(risk);
            risk.JobPosition = this;
        }

        public void RemoveRisk(Risk risk)
        {
            Risks.Remove(risk);
            risk.JobPosition = null;
        }
    }
}
