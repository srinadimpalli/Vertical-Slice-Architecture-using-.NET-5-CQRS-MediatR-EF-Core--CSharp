using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VerticalArchApp.API.Data;
using VerticalArchApp.API.Domain;
using VerticalArchApp.API.Services;

namespace VerticalArchApp.API.Features.Companies
{
    public class GetAllCompanies
    {
        public class Query : IRequest<IEnumerable<Result>> { }
        public class Result
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Country { get; set; }
        }
        //We have a MapperProfile class for each use case in this project, but we could share one
        //per feature instead, moving the MapperProfile class to the same level as the controller
        //public class MapperProfile : Profile
        //{
        //    public MapperProfile()
        //    {
        //        CreateMap<Company, Result>();
        //    }
        //}
        public class Handler : IRequestHandler<Query, IEnumerable<Result>>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;

            public Handler(IServiceManager serviceManager, IMapper mapper)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
            }
            public async Task<IEnumerable<Result>> Handle(Query request, CancellationToken cancellationToken)
            {
                var companies = await _serviceManager.Company.GetAllCompaniesAsync(trackChanges: false);
                var results = _mapper.Map<IEnumerable<Result>>(companies);
                return results;
            }
        }
    }
}
