using MediatR;
using PierreMilo.Application.Services;
using PierreMilo.Domain.Repositories;
using PierreMilo.Domain.Responses.Auth;
using PierreMilo.Domain.Services;

namespace PierreMilo.Application.Commands.Auth.LoginAdmin
{
    public class LoginAdminCommandHandler : IRequestHandler<LoginAdminCommand, LoginResponse>
    {
        private readonly IAuthRepository authRepository;
        private readonly IJwtService jwtService;
        public LoginAdminCommandHandler(IAuthRepository authRepository, IJwtService jwtService)
        {
            this.authRepository = authRepository;
            this.jwtService = jwtService;
        }
        public async Task<LoginResponse> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
        {
            var usuario = await authRepository.LoginAdmin(request);
            string token = jwtService.GenerateToken(usuario);
            return new() { Token = token };
        }
    }
}
