using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VerticalArchApp.API.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
