using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VerticalArchApp.API.Data;
using VerticalArchApp.API.Services;

namespace VerticalArchApp.API.Features.Employees
{
    public class DeleteEmployeeForCompany
    {
        public class DelCommand : IRequest<Unit>
        {
            public int CompanyId { get; set; }
            public int EmployeeId { get; set; }
        }

        public class Validator : AbstractValidator<DelCommand>
        {
            public Validator()
            {
                RuleFor(x => x.CompanyId).GreaterThan(0);
                RuleFor(x => x.EmployeeId).GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<DelCommand, Unit>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;
            public Handler(IServiceManager serviceManager, IMapper mapper)
            {
                _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<Unit> Handle(DelCommand request, CancellationToken cancellationToken)
            {
                var company = await _serviceManager.Company.GetCompanyAsync(request.CompanyId, trackChanges: false);
                if (company == null)
                {
                    throw new NoCompanyExistsException(request.CompanyId);
                }

                var employee = await _serviceManager.Employee.GetEmployeeAsync(request.CompanyId, request.EmployeeId, trackChanges: true);
                if (employee == null)
                {
                    throw new NoEmployeeExistsException(request.CompanyId, request.EmployeeId);
                }
                _serviceManager.Employee.DeleteEmployee(employee);
                await _serviceManager.SaveAsync();

                return Unit.Value;

            }
        }
    }
}
