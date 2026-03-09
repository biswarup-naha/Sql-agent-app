using System.Text;
using Dapper;
using Npgsql;

namespace SqlAgent.Services
{
    public class SchemaService
    {
        private readonly string _connection;

        public SchemaService(IConfiguration config)
        {
            _connection = config.GetConnectionString("Default");
        }

        public async Task<string> GetDatabaseSchema()
        {
            using var conn = new NpgsqlConnection(_connection);

            var tables = await conn.QueryAsync(@"
                SELECT table_name, column_name, data_type
                FROM information_schema.columns
                WHERE table_schema = 'public'
                ORDER BY table_name");

            var grouped = tables.GroupBy(x => x.table_name);

            StringBuilder schema = new();

            foreach (var table in grouped)
            {
                schema.AppendLine($"Table: {table.Key}");

                foreach (var col in table)
                {
                    schema.AppendLine($"  - {col.column_name} ({col.data_type})");
                }

                schema.AppendLine();
            }

            return schema.ToString();
        }
    }
}