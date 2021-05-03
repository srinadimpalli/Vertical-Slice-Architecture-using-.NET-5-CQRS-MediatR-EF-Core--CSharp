using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VerticalArchApp.API.Domain
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
