using MediatR;
using Microsoft.AspNetCore.Mvc;
using PierreMilo.Application.Commands.Auth.LoginAdmin;
using PierreMilo.Domain.Responses.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace PierreMilo.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;
        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        [Route("loginAdmin")]
        [SwaggerResponse(statusCode: 200, type: typeof(LoginResponse))]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginAdminCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
