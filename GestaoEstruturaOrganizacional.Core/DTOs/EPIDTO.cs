namespace GestaoEstruturaOrganizacional.Core.DTOs
{
    public class EPIDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Type { get; set; }
        public string? Description { get; set; }
        public int? ReplacementPeriod { get; set; }
        public string? CANumber { get; set; }
        public int JobPositionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
