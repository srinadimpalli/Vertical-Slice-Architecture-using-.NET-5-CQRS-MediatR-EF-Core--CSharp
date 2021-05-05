using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerticalArchApp.API.Data;
using VerticalArchApp.API.Domain;
using VerticalArchApp.API.Services;

namespace VerticalArchApp.API.Featurers.Companies
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
        Task<Company> GetCompanyAsync(int companyId, bool trackChanges);
    }
    public class CompanyService : ServiceBase<Company>, ICompanyService
    {
        public CompanyService(CompanyEmpContext companyEmpContext) : base(companyEmpContext)
        {

        }
        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                    .OrderBy(c => c.Name)
                    .ToListAsync();
        }

        public async Task<Company> GetCompanyAsync(int companyId, bool trackChanges)
        {
            return await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
                          .SingleOrDefaultAsync();
        }
    }
}
