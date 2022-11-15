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
        public AuthRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<UsuarioModel> LoginAdmin(LoginAdminRequest request)
        {
            string sql = $@"SELECT Id, Password, Correo, IdRol, Estado
                            FROM Usuario
                            WHERE Correo = @Correo AND Status = 1";
            using var connection = context.CreateConnection();
            var user = await connection.QueryFirstOrDefaultAsync<UsuarioModel>(sql, request);
            if (user != null)
            {
                if (!user.Estado)
                {
                    throw new UnauthorizedException("El usuario ha sido desactivado por un administrador");
                }
                else if (request.Password != Handlers.Decrypt(user.Password))
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
