using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static VerticalArchApp.API.Featurers.Employees.ListAllEmployeesForCompany;

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

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<EmpResult>>> GetEmployeesForCompany(int companyid)
        {
            var query = new Query
            {
                CompanyId = companyid
            };
            var results = await _mediator.Send(query);
            return Ok(results);
        }

        [HttpPost()]
        public async Task<ActionResult> CreateEmployee([FromBody] CreateEmployeeForCompany.Command command)
        {
            try
            {
                var result = await _mediator.Send(command);
                //return Ok(result);
                return CreatedAtRoute("EmployeeById", new { id = result.Id }, result);
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
        public async Task<ActionResult> UpdateEmployeeForComapny(int companyid, [FromBody] UpdateEmployeeForCompany.UpdateCommand command)
        {
            try
            {
                command.CompanyId = companyid;
                await _mediator.Send(command);
                return NoContent();
                //return Ok(result);
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
        [HttpDelete("{companyid}")]
        public async Task<ActionResult> DeleteEmployeeForCompany(int companyid, [FromBody] DeleteEmployeeForCompany.DelCommand command)
        {
            try
            {
                command.CompanyId = companyid;
                await _mediator.Send(command);
                return NoContent();
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
