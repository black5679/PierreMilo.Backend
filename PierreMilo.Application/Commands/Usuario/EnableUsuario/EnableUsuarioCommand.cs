using MediatR;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Requests.Usuario;

namespace PierreMilo.Application.Commands.Usuario.EnableUsuario
{
    public class EnableUsuarioCommand : IRequest<Response>
    {
        public int Id { get; set; }
    }
}
