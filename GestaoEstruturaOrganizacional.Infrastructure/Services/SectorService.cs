using GestaoEstruturaOrganizacional.Core.DTOs;
using GestaoEstruturaOrganizacional.Core.Entities;
using GestaoEstruturaOrganizacional.Core.Exceptions;
using GestaoEstruturaOrganizacional.Core.Interfaces;

namespace GestaoEstruturaOrganizacional.Infrastructure.Services
{
    public class SectorService : ISectorService
    {
        private readonly IRepository<Sector> _sectorRepository;

        public SectorService(IRepository<Sector> sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task<IEnumerable<SectorDTO>> GetAllSectorsAsync()
        {
            var sectors = await _sectorRepository.GetAllAsync();
            return sectors.Select(MapToDTO);
        }

        public async Task<SectorDTO?> GetSectorByIdAsync(int id)
        {
            var sector = await _sectorRepository.GetByIdAsync(id);
            return sector != null ? MapToDTO(sector) : null;
        }

        public async Task<SectorDTO> CreateSectorAsync(SectorDTO sectorDTO)
        {
            var sector = MapToEntity(sectorDTO);
            sector.CreatedAt = DateTime.UtcNow;
            sector.UpdatedAt = DateTime.UtcNow;
            var created = await _sectorRepository.AddAsync(sector);
            return MapToDTO(created);
        }

        public async Task<SectorDTO> UpdateSectorAsync(int id, SectorDTO sectorDTO)
        {
            var existing = await _sectorRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ResourceNotFoundException("Sector", "id", id);

            existing.Name = sectorDTO.Name;
            existing.Description = sectorDTO.Description;
            existing.AreaId = sectorDTO.AreaId;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _sectorRepository.UpdateAsync(existing);
            return MapToDTO(updated);
        }

        public async Task DeleteSectorAsync(int id)
        {
            var existing = await _sectorRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ResourceNotFoundException("Sector", "id", id);
            await _sectorRepository.DeleteAsync(id);
        }

        private static SectorDTO MapToDTO(Sector sector)
        {
            return new SectorDTO
            {
                Id = sector.Id,
                Name = sector.Name,
                Description = sector.Description,
                AreaId = sector.AreaId,
                CreatedAt = sector.CreatedAt,
                UpdatedAt = sector.UpdatedAt
            };
        }

        private static Sector MapToEntity(SectorDTO dto)
        {
            return new Sector
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                AreaId = dto.AreaId
            };
        }
    }
}

