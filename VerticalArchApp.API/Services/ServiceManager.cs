using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerticalArchApp.API.Data;
using VerticalArchApp.API.Features.Companies;
using VerticalArchApp.API.Features.Employees;

namespace VerticalArchApp.API.Services
{
    #region Interface
    public interface IServiceManager
    {
        ICompanyService Company { get; }
        IEmployeeService Employee { get; }
        Task SaveAsync();
    }
    #endregion

    #region Implementation
    public class ServiceManager : IServiceManager
    {
        private readonly CompanyEmpContext _companyEmpContext;
        private ICompanyService _companyService;
        private IEmployeeService _employeeService;
        public ServiceManager(CompanyEmpContext companyEmpContext)
        {
            _companyEmpContext = companyEmpContext;
        }
        public ICompanyService Company
        {
            get
            {
                if (_companyService == null)
                    _companyService = new CompanyService(_companyEmpContext);
                return _companyService;
            }
        }

        public IEmployeeService Employee
        {
            get
            {
                if (_employeeService == null)
                    _employeeService = new EmployeeService(_companyEmpContext);
                return _employeeService;
            }
        }

        public Task SaveAsync() => _companyEmpContext.SaveChangesAsync();
    }
    #endregion

}
