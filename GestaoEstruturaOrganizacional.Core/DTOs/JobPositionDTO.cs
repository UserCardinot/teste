namespace GestaoEstruturaOrganizacional.Core.DTOs
{
    public class JobPositionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? CBOCode { get; set; }
        public string? Description { get; set; }
        public int RoleId { get; set; }
        public int? ReportingToId { get; set; }
        public string? EducationLevel { get; set; }
        public string? Experience { get; set; }
        public string? Skills { get; set; }
        public string? Competencies { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<EPIDTO> EPIs { get; set; } = new List<EPIDTO>();
        public List<RiskDTO> Risks { get; set; } = new List<RiskDTO>();
    }
}
