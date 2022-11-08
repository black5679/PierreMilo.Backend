using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace PierreMilo.Infrastructure
{
    public sealed class DataContext : IDisposable
    {
        private readonly IConfiguration _configuration;
        private DbConnection _connection { get; set; }
        private DbTransaction _transaction { get; set; }
        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbConnection Connection => _connection;
        public DbTransaction Transaction => _transaction;
        public DbConnection CreateConnection()
        {
            _connection = new SqlConnection(GetConnectionString());
            return _connection;
        }
        public string? GetConnectionString()
        {
            var connectionString = _configuration.GetValue<string>("ConnectionStrings:Database") ?? "";
            return connectionString;
        }
        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
        }
        public async Task BeginAsync()
        {
            _transaction = await _connection.BeginTransactionAsync();
        }
        public void Commit()
        {
            _transaction.Commit();
        }
        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }

            _transaction = null;
        }
        public void Rollback()
        {
            _transaction.Rollback();
        }
        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }
    }
}

