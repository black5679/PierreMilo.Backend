using MediatR;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Repositories;
using PierreMilo.Domain.Responses.Auth;

namespace PierreMilo.Application.Commands.Usuario.InsertUsuario
{
    public class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, Response>
    {
        private readonly IUsuarioRepository usuarioRepository;
        public UpdateUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }
        public async Task<Response> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await usuarioRepository.Update(request);
            return usuario;
        }
    }
}
