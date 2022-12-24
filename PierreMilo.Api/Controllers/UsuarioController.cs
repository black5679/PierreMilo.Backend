using Microsoft.AspNetCore.Mvc;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using PierreMilo.Domain.Responses.Usuario;
using PierreMilo.Domain.Common;
using PierreMilo.Application.Queries.Usuario.GetAllUsuarioPaginate;
using PierreMilo.Application.Commands.Usuario.InsertUsuario;
using PierreMilo.Application.Commands.Usuario.EnableUsuario;
using PierreMilo.Application.Commands.Usuario.DisableUsuario;
using PierreMilo.Application.Commands.Usuario.UpdateUsuario;
using PierreMilo.Application.Commands.Usuario.DeleteUsuario;
using PierreMilo.Application.Queries.Usuario.GetByIdUsuario;
using PierreMilo.Domain.Requests.Usuario;

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
        [HttpGet("{Id}")]
        [SwaggerResponse(statusCode: 200, type: typeof(UsuarioResponse))]
        public async Task<IActionResult> GetById([FromRoute] GetByIdUsuarioQuery query)
        {
            return Ok(await mediator.Send(query));
        }
        [HttpPost]
        [SwaggerResponse(statusCode: 200, type: typeof(Response))]
        public async Task<IActionResult> Insert([FromBody] InsertUsuarioCommand command)
        {
            return Ok(await mediator.Send(command));
        }
        [HttpPut("{Id}")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response))]
        public async Task<IActionResult> Update([FromRoute]int Id, UpdateUsuarioCommand request)
        {
            request.Id = Id;
            return Ok(await mediator.Send(request));
        }
        [HttpPut()]
        [Route("disable")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response))]
        public async Task<IActionResult> Disable(int Id)
        {
            return Ok(await mediator.Send(new DisableUsuarioCommand { Id = Id }));
        }
        [HttpPut]
        [Route("enable")]
        [SwaggerResponse(statusCode: 200, type: typeof(Response))]
        public async Task<IActionResult> Enable([FromBody] EnableUsuarioCommand command)
        {
            return Ok(await mediator.Send(command));
        }
        [HttpDelete]
        [SwaggerResponse(statusCode: 200, type: typeof(Response))]
        public async Task<IActionResult> Delete([FromBody] DeleteUsuarioCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
