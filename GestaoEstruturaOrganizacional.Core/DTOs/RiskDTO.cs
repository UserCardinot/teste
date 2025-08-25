namespace GestaoEstruturaOrganizacional.Core.DTOs
{
    public class RiskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Severity { get; set; }
        public string? ControlMeasures { get; set; }
        public int JobPositionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
