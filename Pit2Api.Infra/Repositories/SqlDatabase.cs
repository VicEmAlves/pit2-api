using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Pit2Api.Model;

namespace Pit2Api.Infra.Repositories
{
    public class SqlDatabase(IOptions<Config> _config) : ISqlDatabase
    {
        private readonly string _connectionString = _config.Value.ConnectionString;

        public async Task<IEnumerable<T>> QueryManyAsync<T>(string sql, object? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("SQL must be provided", nameof(sql));

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var result = await connection.QueryAsync<T>(sql, parameters);

            // ensure connection is closed before returning
            await connection.CloseAsync();
            return result;
        }

        public async Task<int> ExecuteAsync(string sql, object? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("SQL must be provided", nameof(sql));

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var affected = await connection.ExecuteAsync(sql, parameters);

            // ensure connection is closed before returning
            await connection.CloseAsync();
            return affected;
        }

        public async Task<T> QueryOneAsync<T>(string sql, object? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("SQL must be provided", nameof(sql));

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            // Use QuerySingleAsync to mirror expectation of a single result (will throw if 0 or >1 rows)
            var item = await connection.QuerySingleAsync<T>(sql, parameters);

            // ensure connection is closed before returning
            await connection.CloseAsync();
            return item;
        }
    }
}
