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
            var res = await conn.QueryAsync(query);

            return JsonSerializer.Serialize(res);
        }
    }
}
