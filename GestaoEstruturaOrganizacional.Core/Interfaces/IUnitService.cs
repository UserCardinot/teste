using GestaoEstruturaOrganizacional.Core.DTOs;

namespace GestaoEstruturaOrganizacional.Core.Interfaces
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitDTO>> GetAllUnitsAsync();
        Task<UnitDTO?> GetUnitByIdAsync(int id);
        Task<UnitDTO> CreateUnitAsync(UnitDTO unitDTO);
        Task<UnitDTO> UpdateUnitAsync(int id, UnitDTO unitDTO);
        Task DeleteUnitAsync(int id);
    }
}

