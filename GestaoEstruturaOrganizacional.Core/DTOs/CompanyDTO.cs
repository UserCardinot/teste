namespace GestaoEstruturaOrganizacional.Core.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? CNPJ { get; set; }
        public string? Address { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<UnitDTO> Units { get; set; } = new List<UnitDTO>();
    }
}
