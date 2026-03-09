namespace SqlAgent.Utils
{
    public static class SqlValidator
    {
        public static void Validate(string sql)
        {
            var forbidden = new[]
            {
            "DROP",
            "DELETE",
            "UPDATE",
            "INSERT",
            "ALTER",
            "TRUNCATE"
        };

            foreach (var word in forbidden)
            {
                if (sql.ToUpper().Contains(word))
                {
                    throw new Exception("Dangerous SQL detected.");
                }
            }
        }
    }
}
