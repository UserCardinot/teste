using GestaoEstruturaOrganizacional.Core.DTOs;
using GestaoEstruturaOrganizacional.Core.Entities;
using GestaoEstruturaOrganizacional.Core.Exceptions;
using GestaoEstruturaOrganizacional.Core.Interfaces;

namespace GestaoEstruturaOrganizacional.Infrastructure.Services
{
    public class AreaService : IAreaService
    {
        private readonly IRepository<Area> _areaRepository;

        public AreaService(IRepository<Area> areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task<IEnumerable<AreaDTO>> GetAllAreasAsync()
        {
            var areas = await _areaRepository.GetAllAsync();
            return areas.Select(MapToDTO);
        }

        public async Task<AreaDTO?> GetAreaByIdAsync(int id)
        {
            var area = await _areaRepository.GetByIdAsync(id);
            return area != null ? MapToDTO(area) : null;
        }

        public async Task<AreaDTO> CreateAreaAsync(AreaDTO areaDTO)
        {
            var area = MapToEntity(areaDTO);
            area.CreatedAt = DateTime.UtcNow;
            area.UpdatedAt = DateTime.UtcNow;
            var created = await _areaRepository.AddAsync(area);
            return MapToDTO(created);
        }

        public async Task<AreaDTO> UpdateAreaAsync(int id, AreaDTO areaDTO)
        {
            var existing = await _areaRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ResourceNotFoundException("Area", "id", id);

            existing.Name = areaDTO.Name;
            existing.Description = areaDTO.Description;
            existing.UnitId = areaDTO.UnitId;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _areaRepository.UpdateAsync(existing);
            return MapToDTO(updated);
        }

        public async Task DeleteAreaAsync(int id)
        {
            var existing = await _areaRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ResourceNotFoundException("Area", "id", id);
            await _areaRepository.DeleteAsync(id);
        }

        private static AreaDTO MapToDTO(Area area)
        {
            return new AreaDTO
            {
                Id = area.Id,
                Name = area.Name,
                Description = area.Description,
                UnitId = area.UnitId,
                CreatedAt = area.CreatedAt,
                UpdatedAt = area.UpdatedAt
            };
        }

        private static Area MapToEntity(AreaDTO dto)
        {
            return new Area
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                UnitId = dto.UnitId
            };
        }
    }
}

