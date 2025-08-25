using GestaoEstruturaOrganizacional.Core.DTOs;

namespace GestaoEstruturaOrganizacional.Core.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllRolesAsync();
        Task<RoleDTO?> GetRoleByIdAsync(int id);
        Task<RoleDTO> CreateRoleAsync(RoleDTO roleDTO);
        Task<RoleDTO> UpdateRoleAsync(int id, RoleDTO roleDTO);
        Task DeleteRoleAsync(int id);
    }
}

