using MediatR;
using Microsoft.AspNetCore.Mvc;
using PierreMilo.Application.Queries.Rol.GetAllRol;
using PierreMilo.Domain.Common;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PierreMilo.Api.Controllers
{
    [Route("api/rol")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IMediator mediator;
        public RolController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [SwaggerResponse(statusCode: 200, type: typeof(List<CommonResponse>))]
        public async Task<IActionResult> GetAll([FromQuery] GetAllRolQuery query)
        {
            return Ok(await mediator.Send(query));
        }
    }
}
