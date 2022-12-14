using FluentValidation;
using MediatR;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Requests.Usuario;

namespace PierreMilo.Application.Commands.Usuario.InsertUsuario
{
    public class InsertUsuarioCommand : InsertUsuarioRequest, IRequest<Response>
    {
    }
    public class Validator : AbstractValidator<InsertUsuarioCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Nombres)
                .NotEmpty().WithMessage("Los nombres son obligatorios");
        }
    }
}
