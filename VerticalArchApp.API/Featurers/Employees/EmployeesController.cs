using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VerticalArchApp.API.Featurers.Employees
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost()]
        public async Task<ActionResult<IEnumerable<CreateEmployeeForCompany.EmpResult>>> CreateEmployee([FromBody] CreateEmployeeForCompany.Command command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (NoCompanyExistsException ex)
            {
                return Conflict(new
                {
                    ex.Message,
                });
            }

        }
        [HttpPost("{companyid}")]
        public async Task<ActionResult<IEnumerable<UpdateEmployeeForCompany.UpdateResult>>> UpdateEmployeeForComapny(int companyid, [FromBody] UpdateEmployeeForCompany.UpdateCommand command)
        {
            try
            {
                command.CompanyId = companyid;
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (NoCompanyExistsException ex)
            {
                return Conflict(new
                {
                    ex.Message,
                });
            }
            catch (NoEmployeeExistsException ex)
            {
                return Conflict(new
                {
                    ex.Message,
                    ex.CompanyId,
                    ex.EmployeeId
                });
            }
        }
    }
}
