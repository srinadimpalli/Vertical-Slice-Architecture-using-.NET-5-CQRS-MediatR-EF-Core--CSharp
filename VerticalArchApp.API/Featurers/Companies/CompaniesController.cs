using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VerticalArchApp.API.Featurers.Companies
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
        public async Task<ActionResult<IEnumerable<ListAllCompanies.Result>>> GetAsync()
        {
            var result = await _mediator.Send(new ListAllCompanies.Query());
            return Ok(result);
        }
    }
}
