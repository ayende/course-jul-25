using System.Linq;
using NHibernate.Cfg;
using Raven.Client.Util;

namespace MediaMind.Course.Infrastructure
{
    public class NamingConvention : INamingStrategy
    {
        public string ClassToTableName(string className)
        {
            return "mmn_" + Inflector.Pluralize(className.Split('.').Last());
        }

        public string PropertyToColumnName(string propertyName)
        {
            return propertyName;
        }

        public string TableName(string tableName)
        {
            return "mmn_" + tableName;
        }

        public string ColumnName(string columnName)
        {
            return columnName;
        }

        public string PropertyToTableName(string className, string propertyName)
        {
            return className + "_" + propertyName;
        }

        public string LogicalColumnName(string columnName, string propertyName)
        {
            return columnName;
        }
    }
}