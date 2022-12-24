using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PierreMilo.Domain.Responses.Usuario;

namespace PierreMilo.Application.Queries.Usuario.GetByIdUsuario
{
    public class GetByIdUsuarioQuery : IRequest<UsuarioResponse>
    {
        [BindRequired]
        public int Id { get; set; }
    }
}
