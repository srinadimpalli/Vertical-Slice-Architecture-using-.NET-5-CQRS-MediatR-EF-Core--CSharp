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

namespace VerticalArchApp.API.Featurers.Employees
{
    public class UpdateEmployeeForCompany
    {
        public class UpdateCommand : IRequest<Unit>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Position { get; set; }
            public int CompanyId { get; set; }

        }

        public class UpdateResult
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Position { get; set; }
            public int CompanyId { get; set; }
        }
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<Employee, UpdateResult>();
            }
        }

        public class Validator : AbstractValidator<UpdateCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotNull();
                RuleFor(x => x.CompanyId).NotNull();
            }
        }
        public class Handler : IRequestHandler<UpdateCommand>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;
            public Handler(IServiceManager serviceManager, IMapper mapper)
            {
                _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            async Task<Unit> IRequestHandler<UpdateCommand, Unit>.Handle(UpdateCommand request, CancellationToken cancellationToken)
            {
                // get the company with company id from reqeust
                var company = await _serviceManager.Company.GetCompanyAsync(request.CompanyId, trackChanges: false);
                if (company == null)
                {
                    throw new NoCompanyExistsException(request.CompanyId);
                }
                var employee = await _serviceManager.Employee.GetEmployeeAsync(request.CompanyId, request.Id, trackChanges: true);
                if (employee == null)
                {
                    throw new NoEmployeeExistsException(request.CompanyId, request.Id);
                }
                // update employee entity
                employee.Name = request.Name;
                employee.Position = request.Position;
                employee.Age = request.Age;
                await _serviceManager.SaveAsync();
                return Unit.Value;
            }
        }
    }
}
