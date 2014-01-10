using System;
using System.Collections.Generic;

using Dapper.Oracle.Example.Interface;

using Oracle.ManagedDataAccess.Client;

namespace Dapper.Oracle.Example.Infrastructure
{
    public class Repository
    {
        private readonly IDatabaseConfiguration _databaseConfiguration;

        protected Repository(IDatabaseConfiguration databaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration;
        }

        public virtual IEnumerable<TDataModel> Execute<TDataModel>(string sqlCommand, object parameters = null)
        {
            return Execute(connection => connection.Query<TDataModel>(sqlCommand, parameters));
        }

        public virtual void Execute(string sqlCommand, object parameters = null)
        {
            Execute(connection => connection.Query(sqlCommand, parameters));
       }

        private T Execute<T>(Func<OracleConnection, T> executeCommand)
        {
            using (var connection = new OracleConnection(_databaseConfiguration.ConnectionString))
            {
                connection.Open();

                return executeCommand(connection);
            }
        }
    }
}
