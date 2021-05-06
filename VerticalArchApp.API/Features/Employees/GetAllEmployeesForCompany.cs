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
using VerticalArchApp.API.Services;

namespace VerticalArchApp.API.Features.Employees
{
    public class GetAllEmployeesForCompany
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
        // Validator
        public class Validater : AbstractValidator<Query>
        {
            public Validater()
            {
                RuleFor(x => x.CompanyId).GreaterThan(0);
            }
        }
        //Handler
        public class Handler : IRequestHandler<Query, IEnumerable<EmpResult>>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;

            public Handler(IServiceManager serviceManager, IMapper mapper)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
            }

            public async Task<IEnumerable<EmpResult>> Handle(Query request, CancellationToken cancellationToken)
            {
                var company = await _serviceManager.Company.GetCompanyAsync(request.CompanyId, trackChanges: false);
                if (company == null)
                {
                    throw new NoCompanyExistsException(request.CompanyId);
                }
                var employees = await _serviceManager.Employee.GetEmployeesAsync(request.CompanyId, trackChanges: false);

                var results = _mapper.Map<IEnumerable<EmpResult>>(employees); //(_db.Employees.Where(x => x.CompanyId == request.CompanyId));
                return results;
            }
        }
    }
}
