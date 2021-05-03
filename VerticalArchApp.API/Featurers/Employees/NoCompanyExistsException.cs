using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VerticalArchApp.API.Featurers.Employees
{
    public class NoCompanyExistsException : Exception
    {
        public NoCompanyExistsException(int companyId) : base($"Company with id:{companyId} doesn't exist in the database.")
        {

        }

    }
}
