using Microsoft.EntityFrameworkCore;
using GestaoEstruturaOrganizacional.Core.Entities;
using GestaoEstruturaOrganizacional.Core.Interfaces;
using GestaoEstruturaOrganizacional.Infrastructure.Data;

namespace GestaoEstruturaOrganizacional.Infrastructure.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Company?> GetByCNPJAsync(string cnpj)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.CNPJ == cnpj);
        }

        public async Task<IEnumerable<Company>> GetByNameAsync(string name)
        {
            return await _dbSet.Where(c => c.Name.Contains(name)).ToListAsync();
        }
    }
}
