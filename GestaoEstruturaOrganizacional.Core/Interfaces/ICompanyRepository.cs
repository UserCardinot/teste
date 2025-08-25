using GestaoEstruturaOrganizacional.Core.Entities;

namespace GestaoEstruturaOrganizacional.Core.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company?> GetByCNPJAsync(string cnpj);
        Task<IEnumerable<Company>> GetByNameAsync(string name);
    }
}
