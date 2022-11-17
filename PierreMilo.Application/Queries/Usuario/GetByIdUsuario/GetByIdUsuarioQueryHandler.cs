using MediatR;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Repositories;
using PierreMilo.Domain.Responses.Usuario;

namespace PierreMilo.Application.Queries.Usuario.GetByIdUsuario
{
    internal class GetByIdUsuarioQueryHandler : IRequestHandler<GetByIdUsuarioQuery, UsuarioResponse>
    {
        private readonly IUsuarioRepository usuarioRepository;
        public GetByIdUsuarioQueryHandler(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioResponse> Handle(GetByIdUsuarioQuery request, CancellationToken cancellationToken)
        {
            var response = await usuarioRepository.GetById(request.Id);
            return response;
        }
    }
}
