using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VerticalArchApp.API.Data;

namespace VerticalArchApp.API.Featurers.Employees
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
            private readonly CompanyEmpContext _db;
            private readonly IMapper _mapper;
            public Handler(CompanyEmpContext db, IMapper mapper)
            {
                _db = db ?? throw new ArgumentNullException(nameof(db));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<Unit> Handle(DelCommand request, CancellationToken cancellationToken)
            {
                var employee = await _db.Employees.FindAsync(request.EmployeeId);
                if (employee != null)
                {
                    _db.Employees.Remove(employee);
                    _db.SaveChanges();
                }
                else
                {
                    throw new NoEmployeeExistsException(request.CompanyId, request.EmployeeId);
                }

                return Unit.Value;

            }
        }
    }
}
