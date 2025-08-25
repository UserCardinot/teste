using GestaoEstruturaOrganizacional.Core.DTOs;
using GestaoEstruturaOrganizacional.Core.Entities;
using GestaoEstruturaOrganizacional.Core.Exceptions;
using GestaoEstruturaOrganizacional.Core.Interfaces;

namespace GestaoEstruturaOrganizacional.Infrastructure.Services
{
    public class JobPositionService : IJobPositionService
    {
        private readonly IRepository<JobPosition> _jobPositionRepository;

        public JobPositionService(IRepository<JobPosition> jobPositionRepository)
        {
            _jobPositionRepository = jobPositionRepository;
        }

        public async Task<IEnumerable<JobPositionDTO>> GetAllJobPositionsAsync()
        {
            var items = await _jobPositionRepository.GetAllAsync();
            return items.Select(MapToDTO);
        }

        public async Task<JobPositionDTO?> GetJobPositionByIdAsync(int id)
        {
            var item = await _jobPositionRepository.GetByIdAsync(id);
            return item != null ? MapToDTO(item) : null;
        }

        public async Task<JobPositionDTO> CreateJobPositionAsync(JobPositionDTO dto)
        {
            var entity = MapToEntity(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            var created = await _jobPositionRepository.AddAsync(entity);
            return MapToDTO(created);
        }

        public async Task<JobPositionDTO> UpdateJobPositionAsync(int id, JobPositionDTO dto)
        {
            var existing = await _jobPositionRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ResourceNotFoundException("JobPosition", "id", id);

            existing.Name = dto.Name;
            existing.CBOCode = dto.CBOCode;
            existing.Description = dto.Description;
            existing.RoleId = dto.RoleId;
            existing.ReportingToId = dto.ReportingToId;
            existing.EducationLevel = dto.EducationLevel;
            existing.Experience = dto.Experience;
            existing.Skills = dto.Skills;
            existing.Competencies = dto.Competencies;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _jobPositionRepository.UpdateAsync(existing);
            return MapToDTO(updated);
        }

        public async Task DeleteJobPositionAsync(int id)
        {
            var existing = await _jobPositionRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ResourceNotFoundException("JobPosition", "id", id);
            await _jobPositionRepository.DeleteAsync(id);
        }

        private static JobPositionDTO MapToDTO(JobPosition entity)
        {
            var dto = new JobPositionDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                CBOCode = entity.CBOCode,
                Description = entity.Description,
                RoleId = entity.RoleId,
                ReportingToId = entity.ReportingToId,
                EducationLevel = entity.EducationLevel,
                Experience = entity.Experience,
                Skills = entity.Skills,
                Competencies = entity.Competencies,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };

            // Navigation collections may not be loaded with generic repository, so avoid forcing them
            return dto;
        }

        private static JobPosition MapToEntity(JobPositionDTO dto)
        {
            return new JobPosition
            {
                Id = dto.Id,
                Name = dto.Name,
                CBOCode = dto.CBOCode,
                Description = dto.Description,
                RoleId = dto.RoleId,
                ReportingToId = dto.ReportingToId,
                EducationLevel = dto.EducationLevel,
                Experience = dto.Experience,
                Skills = dto.Skills,
                Competencies = dto.Competencies
            };
        }
    }
}

