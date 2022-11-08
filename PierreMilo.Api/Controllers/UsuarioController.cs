using Microsoft.AspNetCore.Mvc;
using MediatR;
using PierreMilo.Application.Queries.Usuario.GetAllUsuarioPaginateQuery;
using Swashbuckle.AspNetCore.Annotations;
using PierreMilo.Domain.Responses.Usuario;
using PierreMilo.Domain.Common;

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
    }
}
