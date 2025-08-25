using GestaoEstruturaOrganizacional.Core.DTOs;
using GestaoEstruturaOrganizacional.Core.Entities;
using GestaoEstruturaOrganizacional.Core.Exceptions;
using GestaoEstruturaOrganizacional.Core.Interfaces;

namespace GestaoEstruturaOrganizacional.Infrastructure.Services
{
    public class UnitService : IUnitService
    {
        private readonly IRepository<Unit> _unitRepository;

        public UnitService(IRepository<Unit> unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<IEnumerable<UnitDTO>> GetAllUnitsAsync()
        {
            var units = await _unitRepository.GetAllAsync();
            return units.Select(MapToDTO);
        }

        public async Task<UnitDTO?> GetUnitByIdAsync(int id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);
            return unit != null ? MapToDTO(unit) : null;
        }

        public async Task<UnitDTO> CreateUnitAsync(UnitDTO unitDTO)
        {
            var unit = MapToEntity(unitDTO);
            unit.CreatedAt = DateTime.UtcNow;
            unit.UpdatedAt = DateTime.UtcNow;
            var created = await _unitRepository.AddAsync(unit);
            return MapToDTO(created);
        }

        public async Task<UnitDTO> UpdateUnitAsync(int id, UnitDTO unitDTO)
        {
            var existing = await _unitRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ResourceNotFoundException("Unit", "id", id);

            existing.Name = unitDTO.Name;
            existing.Address = unitDTO.Address;
            existing.City = unitDTO.City;
            existing.State = unitDTO.State;
            existing.ZipCode = unitDTO.ZipCode;
            existing.CompanyId = unitDTO.CompanyId;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _unitRepository.UpdateAsync(existing);
            return MapToDTO(updated);
        }

        public async Task DeleteUnitAsync(int id)
        {
            var existing = await _unitRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ResourceNotFoundException("Unit", "id", id);
            await _unitRepository.DeleteAsync(id);
        }

        private static UnitDTO MapToDTO(Unit unit)
        {
            return new UnitDTO
            {
                Id = unit.Id,
                Name = unit.Name,
                Address = unit.Address,
                City = unit.City,
                State = unit.State,
                ZipCode = unit.ZipCode,
                CompanyId = unit.CompanyId,
                CreatedAt = unit.CreatedAt,
                UpdatedAt = unit.UpdatedAt
            };
        }

        private static Unit MapToEntity(UnitDTO dto)
        {
            return new Unit
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                CompanyId = dto.CompanyId
            };
        }
    }
}

