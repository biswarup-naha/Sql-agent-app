namespace SqlAgent.Utils
{
    public static class SqlCleaner
    {
        public static string ExtractSql(string aiResponse)
        {
            if (aiResponse.Contains("```"))
            {
                var start = aiResponse.IndexOf("```") + 3;
                var end = aiResponse.LastIndexOf("```");

                var sql = aiResponse.Substring(start, end - start);

                sql = sql.Replace("sql", "", StringComparison.OrdinalIgnoreCase);

                return sql.Trim();
            }

            return aiResponse.Trim();
        }
    }
}
