using System;
using NHibernate.SqlCommand;

namespace MediaMind.Course.Infrastructure
{
    public class Optimizations
    {
        [ThreadStatic]
        private static string text;

        [ThreadStatic] private static string theTable;

        public static IDisposable Use(string table, string optimization)
        {
            text = optimization;
            theTable = table;
            return new Disposable();
        }

        public class Disposable : IDisposable
        {
            public void Dispose()
            {
                text = null;
                theTable = null;
            }
        }

        public static SqlString Apply(SqlString sql)
        {
            if (text == null)
                return sql;
            var s = "from " + theTable;
            var index = sql.IndexOfCaseInsensitive(s);
            var indexOfSpaceAfterAlias = sql.IndexOf(" ", index + s.Length +1, sql.Length, StringComparison.InvariantCultureIgnoreCase);
            if (indexOfSpaceAfterAlias == -1)
                return sql;

            return sql.Insert(indexOfSpaceAfterAlias, text);
        }
    }

   
}