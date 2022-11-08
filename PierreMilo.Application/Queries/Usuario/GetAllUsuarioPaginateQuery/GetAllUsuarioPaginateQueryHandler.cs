using MediatR;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Repositories;
using PierreMilo.Domain.Responses.Usuario;

namespace PierreMilo.Application.Queries.Usuario.GetAllUsuarioPaginateQuery
{
    internal class GetAllUsuarioPaginateQueryHandler : IRequestHandler<GetAllUsuarioPaginateQuery, PaginateResponse<UsuariosResponse>>
    {
        private readonly IUsuarioRepository usuarioRepository;
        public GetAllUsuarioPaginateQueryHandler(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public async Task<PaginateResponse<UsuariosResponse>> Handle(GetAllUsuarioPaginateQuery request, CancellationToken cancellationToken)
        {
            var response = await usuarioRepository.GetAllPaginate(request);
            return response;
        }
    }
}
