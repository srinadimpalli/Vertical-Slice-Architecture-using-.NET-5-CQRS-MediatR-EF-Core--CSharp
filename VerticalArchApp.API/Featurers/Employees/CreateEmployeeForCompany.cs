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
    public class CreateEmployeeForCompany
    {
        public class Command : IRequest<EmpResult>
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Position { get; set; }
            public int CompanyId { get; set; }
        }
        public class EmpResult
        {
            public int Id { get; set; }
            //public string Name { get; set; }
            //public int Age { get; set; }
            //public string Position { get; set; }
            //public int CompanyId { get; set; }
        }
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<Employee, EmpResult>();
            }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotNull().MaximumLength(30).WithMessage("Maximum lenght for the Name is 30 characters.");
                RuleFor(x => x.Age).NotNull();
                RuleFor(x => x.Position).NotNull().MaximumLength(20).WithMessage("Maximum length for the position is 20 characters.");
            }
        }
        public class Handler : IRequestHandler<Command, EmpResult>
        {
            private readonly CompanyEmpContext _db;
            private readonly IMapper _mapper;
            public Handler(CompanyEmpContext db, IMapper mapper)
            {
                _db = db ?? throw new ArgumentNullException(nameof(db));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }
            public async Task<EmpResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var company = await _db.Companies.FindAsync(request.CompanyId);

                if (company == null)
                {
                    throw new NoCompanyExistsException(request.CompanyId);
                }
                var employee = new Employee()
                {
                    Name = request.Name,
                    Age = request.Age,
                    CompanyId = request.CompanyId,
                    Position = request.Position
                };
                _db.Employees.Add(employee);
                await _db.SaveChangesAsync();
                //return Unit.Value;
                //var resultByCompany = _db.Employees.Where(e => e.CompanyId == request.CompanyId).ToList();
                var result = _mapper.Map<EmpResult>(employee);
                return result;
            }
        }
    }
}
