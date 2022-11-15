using MediatR;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Repositories;
using PierreMilo.Domain.Responses.Auth;

namespace PierreMilo.Application.Commands.Usuario.DeleteUsuario
{
    public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, Response>
    {
        private readonly IUsuarioRepository usuarioRepository;
        public DeleteUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }
        public async Task<Response> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await usuarioRepository.Delete(request.Id);
            return usuario;
        }
    }
}
