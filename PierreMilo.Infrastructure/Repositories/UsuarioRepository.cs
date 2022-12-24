using PierreMilo.Domain.Common;
using PierreMilo.Domain.Repositories;
using Dapper;
using PierreMilo.Domain.Responses.Usuario;
using PierreMilo.Domain.Requests.Usuario;
using PierreMilo.Infrastructure.Common;
using PierreMilo.Infrastructure.Exceptions;
using PierreMilo.Domain.Enums;
using System.Data;
using PierreMilo.Domain.Models;
using System.Reflection;

namespace PierreMilo.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        readonly DataContext context;
        public UsuarioRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<PaginateResponse<UsuariosResponse>> GetAllPaginate(PaginateRequest request)
        {
            string sql = $@"SELECT COUNT(*) FROM Usuario u WHERE u.Status = 1;";
            sql += $@"SELECT u.Id, u.Nombres, u.Apellidos, u.Dni, u.Correo, u.Celular, u.Foto, u.Estado, Rol = r.Nombre FROM Usuario u INNER JOIN Rol r ON (u.IdRol = r.Id) WHERE u.Status = 1 
            ORDER BY 
                CASE WHEN @Order = 0 THEN
                    CASE 
                        WHEN @OrderBy = 'apellidos' THEN u.Apellidos
                        WHEN @OrderBy = 'nombres' THEN u.Nombres
                        ELSE CONVERT(varchar(50),u.Id)
                    END
                END DESC,
                CASE WHEN @Order = 1 THEN
                    CASE @OrderBy
                        WHEN 'apellidos' THEN u.Apellidos
                        WHEN 'nombres' THEN u.Nombres
                        ELSE CONVERT(varchar(50),u.Id)
                    END
                END ASC
            OFFSET (@Page-1)*@Limit ROWS
            FETCH NEXT @Limit ROWS ONLY;";
            using var connection = context.CreateConnection();
            PaginateResponse<UsuariosResponse> response = new();
            using (var multi = await connection.QueryMultipleAsync(sql, request))
            {
                response.Total = multi.ReadFirst<int>();
                response.Results = multi.Read<UsuariosResponse>();
            }
            return response;
        }
        public async Task<UsuarioResponse> GetById(int id)
        {
            string sql = $@"SELECT u.Id, u.Nombres, u.Apellidos, u.Dni, u.Correo, u.Celular, u.Foto, u.Estado, u.IdRol FROM Usuario u WHERE u.Id = @Id AND u.Status = 1";
            using var connection = context.CreateConnection();
            var response = await connection.QueryFirstOrDefaultAsync<UsuarioResponse>(sql, new { Id = id });
            if (response != null)
            {
                sql = $@"SELECT p.IdVista, p.Visualizar, p.Registrar, p.Editar, p.Eliminar FROM PERMISO p WHERE p.IdUsuario = @IdUsuario";
                response.Permisos = (await connection.QueryAsync<PermisoResponse>(sql, new { IdUsuario = id })).ToList();
            }
            return response ?? throw new NotFoundException("No se encontró el usuario");
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
            List<PermisoModel> models = new();
            foreach (var permiso in request.Permisos)
            {
                var model = PermisoRequest.ToModel(id,permiso);
                models.Add(model);
            }
            await connection.ExecuteAsync(sql, models);
            return new() { Data = request, Message = "Usuario registrado con éxito" };
        }
        public async Task<Response> Update(UpdateUsuarioRequest request)
        {
            var sql = $@"Update Usuario SET Nombres = @Nombres, Apellidos = @Apellidos, Dni = @Dni, Correo = @Correo, Celular = @Celular, Foto = @Foto, IdRol = @IdRol, UsuarioModificacion = 1 WHERE Id = @Id;";
            using var connection = context.CreateConnection();
            var id = await connection.ExecuteAsync(sql, request);
            if (id <= 0)
                throw new InternalServerErrorException("Error al modificar el usuario");
            sql = $@"Select IdVista from Permiso Where IdUsuario = @IdUsuario";
            List<PermisoModel> permisos = (await connection.QueryAsync<PermisoModel>(sql, new {IdUsuario = id})).ToList();
            List<PermisoModel> updateModels = new();
            List<PermisoModel> insertModels = new();
            foreach (var req in request.Permisos)
            {
                var model = PermisoRequest.ToModel(id, req);
                for (int i = 0; i < permisos.Count; i++)
                {
                    if (permisos[i].IdVista == req.IdVista)
                    {
                        updateModels.Add(model);
                        break;
                    }
                    else if (i == permisos.Count - 1)
                    {
                        insertModels.Add(model);
                        break;
                    }
                }
            }
            sql = $@"Insert into Permiso (IdUsuario, IdVista, Visualizar, Registrar, Editar, Eliminar) values (@IdUsuario, @IdVista, @Visualizar, @Registrar, @Editar, @Eliminar)";
            await connection.ExecuteAsync(sql, insertModels);
            sql = $@"Update Permiso set IdUsuario = @IdUsuario, IdVista = @IdVista, Visualizar = @Visualizar, Registrar = @Registrar, Editar = @Editar, Eliminar = @Eliminar Where IdUsuario = @IdUsuario And IdVista = @IdVista";
            await connection.ExecuteAsync(sql, updateModels);
            return new() { Data = request, Message = "Usuario registrado con éxito" };
        }

        public async Task<Response> Disable(int idUsuario)
        {
            var sql = $@"Update Usuario SET Estado = 0 WHERE Id = @Id;";
            using var connection = context.CreateConnection();
            var id = await connection.ExecuteAsync(sql, new {Id = idUsuario});
            if (id <= 0)
                throw new InternalServerErrorException("Error al deshabilitar el usuario");
            return new() { Data = idUsuario, Message = "Usuario deshabilitado con éxito" };
        }
        public async Task<Response> Enable(int idUsuario)
        {
            var sql = $@"Update Usuario SET Estado = 1 WHERE Id = @Id;";
            using var connection = context.CreateConnection();
            var id = await connection.ExecuteAsync(sql, new { Id = idUsuario });
            if (id <= 0)
                throw new InternalServerErrorException("Error al habilitar el usuario");
            return new() { Data = idUsuario, Message = "Usuario habilitado con éxito" };
        }
        public async Task<Response> Delete(int idUsuario)
        {
            var sql = $@"Update Usuario SET Status = 0 WHERE Id = @Id;";
            using var connection = context.CreateConnection();
            var id = await connection.ExecuteAsync(sql, new { Id = idUsuario });
            if (id <= 0)
                throw new InternalServerErrorException("Error al eliminar el usuario");
            return new() { Data = idUsuario, Message = "Usuario eliminado con éxito" };
        }
    }
}
