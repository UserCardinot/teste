using GestaoEstruturaOrganizacional.Core.DTOs;

namespace GestaoEstruturaOrganizacional.Core.Interfaces
{
    public interface IJobPositionService
    {
        Task<IEnumerable<JobPositionDTO>> GetAllJobPositionsAsync();
        Task<JobPositionDTO?> GetJobPositionByIdAsync(int id);
        Task<JobPositionDTO> CreateJobPositionAsync(JobPositionDTO jobPositionDTO);
        Task<JobPositionDTO> UpdateJobPositionAsync(int id, JobPositionDTO jobPositionDTO);
        Task DeleteJobPositionAsync(int id);
    }
}

