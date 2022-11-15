﻿using MediatR;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Repositories;
using PierreMilo.Domain.Responses.Auth;

namespace PierreMilo.Application.Commands.Usuario.DisableUsuario
{
    public class DisableUsuarioCommandHandler : IRequestHandler<DisableUsuarioCommand, Response>
    {
        private readonly IUsuarioRepository usuarioRepository;
        public DisableUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }
        public async Task<Response> Handle(DisableUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await usuarioRepository.Disable(request.Id);
            return usuario;
        }
    }
}
