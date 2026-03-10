using System.Text.Json;
using Dapper;
using Npgsql;

namespace SqlAgent.Services
{
    public class SqlService
    {
        private readonly string _connectString;

        public SqlService(IConfiguration configuration)
        {
            _connectString = configuration.GetConnectionString("Default");
        }

        public async Task<string> ExecuteQuery(string query)
        {
            using var conn = new NpgsqlConnection(_connectString);

            var normalized = query.TrimStart().ToUpper();

            // SELECT queries
            if (normalized.StartsWith("SELECT"))
            {
                var res = await conn.QueryAsync(query);
                return JsonSerializer.Serialize(res);
            }

            // DML / DDL queries
            else
            {
                var affected = await conn.ExecuteAsync(query);

                return JsonSerializer.Serialize(new
                {
                    success = true,
                    rowsAffected = affected,
                    message = "Command executed successfully"
                });
            }
        }
    }
}