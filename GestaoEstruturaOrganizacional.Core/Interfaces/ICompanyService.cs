using GestaoEstruturaOrganizacional.Core.DTOs;

namespace GestaoEstruturaOrganizacional.Core.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync();
        Task<CompanyDTO?> GetCompanyByIdAsync(int id);
        Task<CompanyDTO> CreateCompanyAsync(CompanyDTO companyDTO);
        Task<CompanyDTO> UpdateCompanyAsync(int id, CompanyDTO companyDTO);
        Task DeleteCompanyAsync(int id);
    }
}
