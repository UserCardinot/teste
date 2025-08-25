using GestaoEstruturaOrganizacional.Core.DTOs;
using GestaoEstruturaOrganizacional.Core.Entities;
using GestaoEstruturaOrganizacional.Core.Exceptions;
using GestaoEstruturaOrganizacional.Core.Interfaces;

namespace GestaoEstruturaOrganizacional.Infrastructure.Services
{
    public class EPIService : IEPIService
    {
        private readonly IRepository<EPI> _epiRepository;

        public EPIService(IRepository<EPI> epiRepository)
        {
            _epiRepository = epiRepository;
        }

        public async Task<IEnumerable<EPIDTO>> GetAllEPIsAsync()
        {
            var items = await _epiRepository.GetAllAsync();
            return items.Select(MapToDTO);
        }

        public async Task<EPIDTO?> GetEPIByIdAsync(int id)
        {
            var item = await _epiRepository.GetByIdAsync(id);
            return item != null ? MapToDTO(item) : null;
        }

        public async Task<EPIDTO> CreateEPIAsync(EPIDTO dto)
        {
            var entity = MapToEntity(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            var created = await _epiRepository.AddAsync(entity);
            return MapToDTO(created);
        }

        public async Task<EPIDTO> UpdateEPIAsync(int id, EPIDTO dto)
        {
            var existing = await _epiRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ResourceNotFoundException("EPI", "id", id);

            existing.Name = dto.Name;
            existing.Type = dto.Type;
            existing.Description = dto.Description;
            existing.ReplacementPeriod = dto.ReplacementPeriod;
            existing.CANumber = dto.CANumber;
            existing.JobPositionId = dto.JobPositionId;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _epiRepository.UpdateAsync(existing);
            return MapToDTO(updated);
        }

        public async Task DeleteEPIAsync(int id)
        {
            var existing = await _epiRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ResourceNotFoundException("EPI", "id", id);
            await _epiRepository.DeleteAsync(id);
        }

        private static EPIDTO MapToDTO(EPI entity)
        {
            return new EPIDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Type = entity.Type,
                Description = entity.Description,
                ReplacementPeriod = entity.ReplacementPeriod,
                CANumber = entity.CANumber,
                JobPositionId = entity.JobPositionId,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        private static EPI MapToEntity(EPIDTO dto)
        {
            return new EPI
            {
                Id = dto.Id,
                Name = dto.Name,
                Type = dto.Type,
                Description = dto.Description,
                ReplacementPeriod = dto.ReplacementPeriod,
                CANumber = dto.CANumber,
                JobPositionId = dto.JobPositionId
            };
        }
    }
}

