using Microsoft.AspNetCore.Mvc;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using PierreMilo.Domain.Responses.Usuario;
using PierreMilo.Domain.Common;
using PierreMilo.Application.Queries.Usuario.GetAllUsuarioPaginate;
using PierreMilo.Application.Commands.Usuario.InsertUsuario;

namespace PierreMilo.Api.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator mediator;
        public UsuarioController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [Route("paginate")]
        [SwaggerResponse(statusCode: 200, type: typeof(PaginateResponse<UsuariosResponse>))]
        public async Task<IActionResult> GetAllPaginate([FromQuery] GetAllUsuarioPaginateQuery query)
        {
            return Ok(await mediator.Send(query));
        }
        [HttpPost]
        [SwaggerResponse(statusCode: 200, type: typeof(Response))]
        public async Task<IActionResult> Insert([FromBody] InsertUsuarioCommand command)
        {
            return Ok(await mediator.Send(command));
        }
        [HttpPut]
        [SwaggerResponse(statusCode: 200, type: typeof(Response))]
        public async Task<IActionResult> Update([FromBody] UpdateUsuarioCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
