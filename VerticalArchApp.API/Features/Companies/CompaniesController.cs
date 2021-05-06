using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VerticalArchApp.API.Features.Companies
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllCompanies.Result))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GetAllCompanies.Result>>> GetAsync()
        {
            var result = await _mediator.Send(new GetAllCompanies.Query());
            return Ok(result);
        }
    }
}
