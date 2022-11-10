using MediatR;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Requests.Usuario;

namespace PierreMilo.Application.Commands.Usuario.InsertUsuario
{
    public class InsertUsuarioCommand : InsertUsuarioRequest, IRequest<Response>
    {
    }
}
