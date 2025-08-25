using GestaoEstruturaOrganizacional.Core.DTOs;

namespace GestaoEstruturaOrganizacional.Core.Interfaces
{
    public interface IRiskService
    {
        Task<IEnumerable<RiskDTO>> GetAllRisksAsync();
        Task<RiskDTO?> GetRiskByIdAsync(int id);
        Task<RiskDTO> CreateRiskAsync(RiskDTO riskDTO);
        Task<RiskDTO> UpdateRiskAsync(int id, RiskDTO riskDTO);
        Task DeleteRiskAsync(int id);
    }
}

