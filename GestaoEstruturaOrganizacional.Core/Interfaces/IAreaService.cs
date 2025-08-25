using GestaoEstruturaOrganizacional.Core.DTOs;

namespace GestaoEstruturaOrganizacional.Core.Interfaces
{
    public interface IAreaService
    {
        Task<IEnumerable<AreaDTO>> GetAllAreasAsync();
        Task<AreaDTO?> GetAreaByIdAsync(int id);
        Task<AreaDTO> CreateAreaAsync(AreaDTO areaDTO);
        Task<AreaDTO> UpdateAreaAsync(int id, AreaDTO areaDTO);
        Task DeleteAreaAsync(int id);
    }
}

