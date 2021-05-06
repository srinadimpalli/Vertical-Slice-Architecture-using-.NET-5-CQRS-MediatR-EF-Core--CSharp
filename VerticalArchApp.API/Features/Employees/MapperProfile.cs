using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerticalArchApp.API.Domain;
using VerticalArchApp.API.Features.Employees;
namespace VerticalArchApp.API.Features.Employees
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<Employee, CreateEmployeeForCompany.EmpResult>();
            CreateMap<Employee, GetAllEmployeesForCompany.EmpResult>();
            CreateMap<Employee, UpdateEmployeeForCompany.UpdateResult>();
        }
    }
}
