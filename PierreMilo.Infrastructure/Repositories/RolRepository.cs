using Dapper;
using PierreMilo.Domain.Common;
using PierreMilo.Domain.Models;
using PierreMilo.Domain.Repositories;

namespace PierreMilo.Infrastructure.Repositories
{
    public class RolRepository : IRolRepository
    {
        readonly DataContext context;
        public RolRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<List<CommonResponse>> GetAll()
        {
            string sql = $@"SELECT Id, Nombre
                            FROM Rol";
            using var connection = context.CreateConnection();
            var vistas = (await connection.QueryAsync<CommonResponse>(sql)).ToList();
            return vistas;
        }
    }
}
