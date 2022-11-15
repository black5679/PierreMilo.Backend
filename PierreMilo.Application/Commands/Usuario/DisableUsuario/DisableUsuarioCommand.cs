using MediatR;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Requests.Usuario;

namespace PierreMilo.Application.Commands.Usuario.DisableUsuario
{
    public class DisableUsuarioCommand : IRequest<Response>
    {
        public int Id { get; set; }
    }
}
