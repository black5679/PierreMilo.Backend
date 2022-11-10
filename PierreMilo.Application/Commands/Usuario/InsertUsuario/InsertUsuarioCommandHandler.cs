using MediatR;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Repositories;
using PierreMilo.Domain.Responses.Auth;

namespace PierreMilo.Application.Commands.Usuario.InsertUsuario
{
    public class InsertUsuarioCommandHandler : IRequestHandler<InsertUsuarioCommand, Response>
    {
        private readonly IUsuarioRepository usuarioRepository;
        public InsertUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }
        public async Task<Response> Handle(InsertUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await usuarioRepository.Insert(request);
            return usuario;
        }
    }
}
