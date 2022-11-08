using Dapper;
using PierreMilo.Domain.Models;
using PierreMilo.Domain.Repositories;
using PierreMilo.Domain.Requests.Auth;
using PierreMilo.Domain.Services;
using PierreMilo.Infrastructure.Common;
using PierreMilo.Infrastructure.Exceptions;

namespace PierreMilo.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        readonly DataContext context;
        readonly IJwtService jwtService;
        public AuthRepository(DataContext context, IJwtService jwtService)
        {
            this.context = context;
            this.jwtService = jwtService;
        }
        public async Task<UsuarioModel> LoginAdmin(LoginAdminRequest usuario)
        {
            string sql = $@"SELECT Id, Password, Correo, IdRol, Estado
                            FROM Usuario
                            WHERE Correo = @Correo AND Status = 1";
            using var connection = context.CreateConnection();
            var user = await connection.QueryFirstOrDefaultAsync<UsuarioModel>(sql, usuario);
            if (user != null)
            {
                if (!user.Estado)
                {
                    throw new UnauthorizedException("El usuario ha sido desactivado por un administrador");
                }
                else if (usuario.Password != Handlers.Decrypt(user.Password))
                {
                    throw new UnauthorizedException("La contraseña es incorrecta");
                }
            }
            else
            {
                throw new UnauthorizedException("El usuario no existe");
            }

            return user;
        }
    }
}
