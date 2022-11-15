using MediatR;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Repositories;
using PierreMilo.Domain.Responses.Auth;

namespace PierreMilo.Application.Commands.Usuario.EnableUsuario
{
    public class EnableUsuarioCommandHandler : IRequestHandler<EnableUsuarioCommand, Response>
    {
        private readonly IUsuarioRepository usuarioRepository;
        public EnableUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }
        public async Task<Response> Handle(EnableUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await usuarioRepository.Enable(request.Id);
            return usuario;
        }
    }
}
