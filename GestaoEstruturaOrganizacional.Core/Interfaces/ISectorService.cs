using GestaoEstruturaOrganizacional.Core.DTOs;

namespace GestaoEstruturaOrganizacional.Core.Interfaces
{
    public interface ISectorService
    {
        Task<IEnumerable<SectorDTO>> GetAllSectorsAsync();
        Task<SectorDTO?> GetSectorByIdAsync(int id);
        Task<SectorDTO> CreateSectorAsync(SectorDTO sectorDTO);
        Task<SectorDTO> UpdateSectorAsync(int id, SectorDTO sectorDTO);
        Task DeleteSectorAsync(int id);
    }
}

