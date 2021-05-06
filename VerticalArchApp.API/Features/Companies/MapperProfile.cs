using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerticalArchApp.API.Domain;
using VerticalArchApp.API.Features.Companies;

namespace VerticalArchApp.API.Features.Companies
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Company, GetAllCompanies.Result>();
        }
    }
}
