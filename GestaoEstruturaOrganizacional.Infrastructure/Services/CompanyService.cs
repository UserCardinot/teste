using GestaoEstruturaOrganizacional.Core.DTOs;
using GestaoEstruturaOrganizacional.Core.Entities;
using GestaoEstruturaOrganizacional.Core.Interfaces;
using GestaoEstruturaOrganizacional.Core.Exceptions;

namespace GestaoEstruturaOrganizacional.Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            return companies.Select(MapToDTO);
        }

        public async Task<CompanyDTO?> GetCompanyByIdAsync(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            return company != null ? MapToDTO(company) : null;
        }

        public async Task<CompanyDTO> CreateCompanyAsync(CompanyDTO companyDTO)
        {
            var company = MapToEntity(companyDTO);
            company.CreatedAt = DateTime.UtcNow;
            company.UpdatedAt = DateTime.UtcNow;
            
            var createdCompany = await _companyRepository.AddAsync(company);
            return MapToDTO(createdCompany);
        }

        public async Task<CompanyDTO> UpdateCompanyAsync(int id, CompanyDTO companyDTO)
        {
            var existingCompany = await _companyRepository.GetByIdAsync(id);
            if (existingCompany == null)
                throw new ResourceNotFoundException("Company", "id", id);

            existingCompany.Name = companyDTO.Name;
            existingCompany.CNPJ = companyDTO.CNPJ;
            existingCompany.Address = companyDTO.Address;
            existingCompany.ContactPerson = companyDTO.ContactPerson;
            existingCompany.ContactEmail = companyDTO.ContactEmail;
            existingCompany.ContactPhone = companyDTO.ContactPhone;
            existingCompany.UpdatedAt = DateTime.UtcNow;

            var updatedCompany = await _companyRepository.UpdateAsync(existingCompany);
            return MapToDTO(updatedCompany);
        }

        public async Task DeleteCompanyAsync(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
                throw new ResourceNotFoundException("Company", "id", id);

            await _companyRepository.DeleteAsync(id);
        }

        private static CompanyDTO MapToDTO(Company company)
        {
            return new CompanyDTO
            {
                Id = company.Id,
                Name = company.Name,
                CNPJ = company.CNPJ,
                Address = company.Address,
                ContactPerson = company.ContactPerson,
                ContactEmail = company.ContactEmail,
                ContactPhone = company.ContactPhone,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt,
                Units = company.Units.Select(u => new UnitDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Address = u.Address,
                    City = u.City,
                    State = u.State,
                    ZipCode = u.ZipCode,
                    CompanyId = u.CompanyId,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt
                }).ToList()
            };
        }

        private static Company MapToEntity(CompanyDTO dto)
        {
            return new Company
            {
                Id = dto.Id,
                Name = dto.Name,
                CNPJ = dto.CNPJ,
                Address = dto.Address,
                ContactPerson = dto.ContactPerson,
                ContactEmail = dto.ContactEmail,
                ContactPhone = dto.ContactPhone
            };
        }
    }
}
