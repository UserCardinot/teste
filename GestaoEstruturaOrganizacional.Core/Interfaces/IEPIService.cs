using GestaoEstruturaOrganizacional.Core.DTOs;

namespace GestaoEstruturaOrganizacional.Core.Interfaces
{
    public interface IEPIService
    {
        Task<IEnumerable<EPIDTO>> GetAllEPIsAsync();
        Task<EPIDTO?> GetEPIByIdAsync(int id);
        Task<EPIDTO> CreateEPIAsync(EPIDTO epiDTO);
        Task<EPIDTO> UpdateEPIAsync(int id, EPIDTO epiDTO);
        Task DeleteEPIAsync(int id);
    }
}

