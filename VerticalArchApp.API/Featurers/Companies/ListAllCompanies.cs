using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VerticalArchApp.API.Data;
using VerticalArchApp.API.Domain;

namespace VerticalArchApp.API.Featurers.Companies
{
    public class ListAllCompanies
    {
        public class Command : IRequest<IEnumerable<Result>> { }
        public class Result
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Country { get; set; }
        }
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<Company, Result>();
            }
        }
        public class Handler : IRequestHandler<Command, IEnumerable<Result>>
        {
            private readonly CompanyEmpContext _db;
            private readonly IMapper _mapper;

            public Handler(CompanyEmpContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }
            public Task<IEnumerable<Result>> Handle(Command request, CancellationToken cancellationToken)
            {
                var results = _mapper.ProjectTo<Result>(_db.Companies);
                return Task.FromResult(results.AsEnumerable());
            }
        }
    }
}
