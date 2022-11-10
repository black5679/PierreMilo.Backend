using PierreMilo.Domain.Common;
using PierreMilo.Domain.Repositories;
using Dapper;
using PierreMilo.Domain.Responses.Usuario;
using PierreMilo.Domain.Requests.Usuario;
using PierreMilo.Infrastructure.Common;
using PierreMilo.Infrastructure.Exceptions;
using PierreMilo.Domain.Enums;
using System.Data;

namespace PierreMilo.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        readonly DataContext context;
        public UsuarioRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<PaginateResponse<UsuariosResponse>> GetAllPaginate(PaginateRequest paginate)
        {
            string sql = $@"SELECT COUNT(*) FROM Usuario u WHERE u.Status = 1;";
            sql += $@"SELECT u.Id, u.Nombres, u.Apellidos, u.Dni, u.Correo, u.Celular, u.Foto, u.Estado, Rol = r.Nombre, r.Nombre FROM Usuario u INNER JOIN Rol r ON (u.IdRol = r.Id) WHERE u.Status = 1 ORDER BY u.Id OFFSET (@Page-1)*@Limit ROWS
            FETCH NEXT @Limit ROWS ONLY;";
            using var connection = context.CreateConnection();
            PaginateResponse<UsuariosResponse> response = new();
            using (var multi = await connection.QueryMultipleAsync(sql, new { paginate.Limit, paginate.Page }))
            {
                response.Total = multi.ReadFirst<int>();
                response.Results = multi.Read<UsuariosResponse>();
            }
            return response;
        }
        public async Task<Response> Insert(InsertUsuarioRequest request)
        {
            var sql = $@"Insert into Usuario (Nombres, Apellidos, Dni, Correo, Celular, Password, Foto, IdRol, UsuarioCreacion, UsuarioModificacion) OUTPUT Inserted.Id values (@Nombres, @Apellidos, @Dni, @Correo, @Celular, @Password, @Foto, @IdRol, 1, 1);";
            using var connection = context.CreateConnection();
            request.Password = Handlers.Encrypt(request.Password);
            var id = await connection.ExecuteScalarAsync<int>(sql, request);
            if (id <= 0)
                throw new InternalServerErrorException("Error al registrar el usuario");
            sql = $@"Insert into Permiso (IdUsuario, IdVista, Visualizar, Registrar, Editar, Eliminar) values (@IdUsuario, @IdVista, @Visualizar, @Registrar, @Editar, @Eliminar)";
            foreach (var item in request.Permisos)
            {
                item.IdUsuario = id;
            }
            await connection.ExecuteAsync(sql, request.Permisos);
            return new() { Data = request, Message = "Usuario registrado con éxito" };
        }
        public async Task<Response> Update(UpdateUsuarioRequest request)
        {
            var sql = $@"Update Usuario SET Nombres = @Nombres, Apellidos = @Apellidos, Dni = @Dni, Correo = @Correo, Celular = @Celular, Foto = @Foto, IdRol = @IdRol, UsuarioModificacion = 1 WHERE Id = @Id;";
            using var connection = context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, request);
            if (affectedRows <= 0)
                throw new InternalServerErrorException("Error al registrar el usuario");
            return new() { Data = request, Message = "Usuario registrado con éxito" };
        }
    }
}
