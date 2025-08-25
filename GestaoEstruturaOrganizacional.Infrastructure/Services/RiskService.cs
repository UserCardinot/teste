using GestaoEstruturaOrganizacional.Core.DTOs;
using GestaoEstruturaOrganizacional.Core.Entities;
using GestaoEstruturaOrganizacional.Core.Exceptions;
using GestaoEstruturaOrganizacional.Core.Interfaces;

namespace GestaoEstruturaOrganizacional.Infrastructure.Services
{
    public class RiskService : IRiskService
    {
        private readonly IRepository<Risk> _riskRepository;

        public RiskService(IRepository<Risk> riskRepository)
        {
            _riskRepository = riskRepository;
        }

        public async Task<IEnumerable<RiskDTO>> GetAllRisksAsync()
        {
            var items = await _riskRepository.GetAllAsync();
            return items.Select(MapToDTO);
        }

        public async Task<RiskDTO?> GetRiskByIdAsync(int id)
        {
            var item = await _riskRepository.GetByIdAsync(id);
            return item != null ? MapToDTO(item) : null;
        }

        public async Task<RiskDTO> CreateRiskAsync(RiskDTO dto)
        {
            var entity = MapToEntity(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            var created = await _riskRepository.AddAsync(entity);
            return MapToDTO(created);
        }

        public async Task<RiskDTO> UpdateRiskAsync(int id, RiskDTO dto)
        {
            var existing = await _riskRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ResourceNotFoundException("Risk", "id", id);

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Category = dto.Category;
            existing.Severity = dto.Severity;
            existing.ControlMeasures = dto.ControlMeasures;
            existing.JobPositionId = dto.JobPositionId;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _riskRepository.UpdateAsync(existing);
            return MapToDTO(updated);
        }

        public async Task DeleteRiskAsync(int id)
        {
            var existing = await _riskRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ResourceNotFoundException("Risk", "id", id);
            await _riskRepository.DeleteAsync(id);
        }

        private static RiskDTO MapToDTO(Risk entity)
        {
            return new RiskDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Category = entity.Category,
                Severity = entity.Severity,
                ControlMeasures = entity.ControlMeasures,
                JobPositionId = entity.JobPositionId,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        private static Risk MapToEntity(RiskDTO dto)
        {
            return new Risk
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Category = dto.Category,
                Severity = dto.Severity,
                ControlMeasures = dto.ControlMeasures,
                JobPositionId = dto.JobPositionId
            };
        }
    }
}

