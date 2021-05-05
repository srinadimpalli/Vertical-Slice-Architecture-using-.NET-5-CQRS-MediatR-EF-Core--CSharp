using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerticalArchApp.API.Data;
using VerticalArchApp.API.Domain;
using VerticalArchApp.API.Services;

namespace VerticalArchApp.API.Featurers.Employees
{
    #region Interface

    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(int companyId, bool trackChanges);
        Task<Employee> GetEmployeeAsync(int companyId, int id, bool trackChanges);
        void CreateEmployeeForCompany(int companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }
    #endregion
    #region Implementation
    public class EmployeeService : ServiceBase<Employee>, IEmployeeService
    {
        public EmployeeService(CompanyEmpContext companyEmpContext) : base(companyEmpContext) { }
        public void CreateEmployeeForCompany(int companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }
        public async Task<IEnumerable<Employee>> GetEmployeesAsync(int companyId, bool trackChanges)
        {
            return await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
                        .OrderBy(e => e.Name)
                        .ToListAsync();
        }
        public async Task<Employee> GetEmployeeAsync(int companyId, int id, bool trackChanges)
        {
            return await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
                   .SingleOrDefaultAsync();
        }


    }
    #endregion
}
