namespace SqlAgent.Utils
{
    public static class SqlValidator
    {
        public static void Validate(string sql, string role)
        {
            sql = sql.ToUpper();

            if (role == "USER")
            {
                var forbidden = new[]
                {
                    "DROP","DELETE","UPDATE","INSERT","ALTER","TRUNCATE"
                };

                CheckForbidden(sql, forbidden);
            }

            else if (role == "ADMIN")
            {
                var forbidden = new[]
                {
                    "DROP","ALTER","TRUNCATE"
                };

                CheckForbidden(sql, forbidden);
            }

            else if (role == "SUPERADMIN")
            {
                // full access
                return;
            }
        }

        private static void CheckForbidden(string sql, string[] words)
        {
            foreach (var word in words)
            {
                if (sql.Contains(word))
                    throw new Exception($"SQL operation '{word}' not allowed for your role.");
            }
        }
    }
}