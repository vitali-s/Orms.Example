using System.Configuration;

using Dapper.Oracle.Example.Interface;

namespace Dapper.Oracle.Example.Infrastructure
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public static readonly string ConnectionStringName = "Oracle11g";

        public DatabaseConfiguration()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        }

        public string ConnectionString { get; private set; }
    }
}
