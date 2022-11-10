using FluentValidation;
using MediatR;
using PierreMilo.Domain.Requests.Auth;
using PierreMilo.Domain.Responses.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierreMilo.Application.Commands.Auth.LoginAdmin
{
    public class LoginAdminCommand : LoginAdminRequest, IRequest<LoginResponse>
    {
        public class Validator : AbstractValidator<LoginAdminCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Correo)
                    .NotEmpty().WithMessage("El usuario no puede ser vacío");
                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("La contraseña no puede ser vacía");
            }
        }
    }
}
