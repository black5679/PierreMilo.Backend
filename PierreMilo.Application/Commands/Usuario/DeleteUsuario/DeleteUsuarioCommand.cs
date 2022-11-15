using MediatR;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Requests.Usuario;

namespace PierreMilo.Application.Commands.Usuario.DeleteUsuario
{
    public class DeleteUsuarioCommand : IRequest<Response>
    {
        public int Id { get; set; }
    }
}
