using GestaoEstruturaOrganizacional.Core.DTOs;
using GestaoEstruturaOrganizacional.Core.Entities;
using GestaoEstruturaOrganizacional.Core.Exceptions;
using GestaoEstruturaOrganizacional.Core.Interfaces;

namespace GestaoEstruturaOrganizacional.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;

        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return roles.Select(MapToDTO);
        }

        public async Task<RoleDTO?> GetRoleByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            return role != null ? MapToDTO(role) : null;
        }

        public async Task<RoleDTO> CreateRoleAsync(RoleDTO roleDTO)
        {
            var role = MapToEntity(roleDTO);
            role.CreatedAt = DateTime.UtcNow;
            role.UpdatedAt = DateTime.UtcNow;
            var created = await _roleRepository.AddAsync(role);
            return MapToDTO(created);
        }

        public async Task<RoleDTO> UpdateRoleAsync(int id, RoleDTO roleDTO)
        {
            var existing = await _roleRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ResourceNotFoundException("Role", "id", id);

            existing.Name = roleDTO.Name;
            existing.Description = roleDTO.Description;
            existing.SectorId = roleDTO.SectorId;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _roleRepository.UpdateAsync(existing);
            return MapToDTO(updated);
        }

        public async Task DeleteRoleAsync(int id)
        {
            var existing = await _roleRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ResourceNotFoundException("Role", "id", id);
            await _roleRepository.DeleteAsync(id);
        }

        private static RoleDTO MapToDTO(Role role)
        {
            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                SectorId = role.SectorId,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt
            };
        }

        private static Role MapToEntity(RoleDTO dto)
        {
            return new Role
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                SectorId = dto.SectorId
            };
        }
    }
}

