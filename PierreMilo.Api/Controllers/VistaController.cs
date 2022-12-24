using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PierreMilo.Application.Commands.Auth.LoginAdmin;
using PierreMilo.Application.Queries.Vista.GetAllVista;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Responses.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace PierreMilo.Api.Controllers
{
    [Route("api/vista")]
    [ApiController]
    public class VistaController : ControllerBase
    {
        private readonly IMediator mediator;
        public VistaController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [SwaggerResponse(statusCode: 200, type: typeof(List<CommonResponse>))]
        public async Task<IActionResult> GetAll([FromQuery] GetAllVistaQuery query)
        {
            return Ok(await mediator.Send(query));
        }
    }
}
