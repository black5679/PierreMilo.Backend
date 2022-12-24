using FluentValidation;
using MediatR;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Requests.Usuario;

namespace PierreMilo.Application.Commands.Usuario.UpdateUsuario
{
    public class UpdateUsuarioCommand : UpdateUsuarioRequest, IRequest<Response>
    {
    }
    public class Validator : AbstractValidator<UpdateUsuarioCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Nombres)
                .NotEmpty().WithMessage("Los nombres son obligatorios");
        }
    }
}
