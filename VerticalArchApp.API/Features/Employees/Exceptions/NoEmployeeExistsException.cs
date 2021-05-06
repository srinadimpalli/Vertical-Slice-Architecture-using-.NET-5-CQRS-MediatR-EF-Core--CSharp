using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VerticalArchApp.API.Features.Employees
{
    public class NoEmployeeExistsException : Exception
    {
        public NoEmployeeExistsException(int companyId, int employeeId) : base($"Employee with id:{employeeId} doesn't exist in the database for company id {companyId}")
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
        }
        public int CompanyId { get; }
        public int EmployeeId { get; }
    }
}
