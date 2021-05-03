using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VerticalArchApp.API.Data;
using VerticalArchApp.API.Domain;

namespace VerticalArchApp.API.Featurers.Employees
{
    public class ListAllEmployeesForCompany
    {
        // Input
        public class Query : IRequest<IEnumerable<EmpResult>>
        {
            public int CompanyId { get; set; }
        }
        //Output
        public class EmpResult
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Position { get; set; }
        }

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<Employee, EmpResult>();
            }
        }
        public class Validater : AbstractValidator<Query>
        {
            public Validater()
            {
                RuleFor(x => x.CompanyId).GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<EmpResult>>
        {
            private readonly CompanyEmpContext _db;
            private readonly IMapper _mapper;

            public Handler(CompanyEmpContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }

            public async Task<IEnumerable<EmpResult>> Handle(Query request, CancellationToken cancellationToken)
            {
                var company = await _db.Companies.FindAsync(request.CompanyId);
                if (company == null)
                {
                    throw new NoCompanyExistsException(request.CompanyId);
                }
                var results = _mapper.ProjectTo<EmpResult>(_db.Employees.Where(x => x.CompanyId == request.CompanyId));
                return results.AsEnumerable();
            }
        }
    }
}
