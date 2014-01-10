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

        protected virtual void Execute(string sqlCommand, object parameters = null)
        {
            Execute(connection => connection.Query(sqlCommand, parameters));
        }

        protected virtual T Execute<T>(Func<OracleConnection, T> executeCommand)
        {
            using (var connection = new OracleConnection(_databaseConfiguration.ConnectionString))
            {
                connection.Open();

                return executeCommand(connection);
            }
        }

        protected virtual void Execute(Action<OracleConnection> executeCommand)
        {
            using (var connection = new OracleConnection(_databaseConfiguration.ConnectionString))
            {
                connection.Open();

                executeCommand(connection);
            }
        }

        protected virtual TResult ExecuteTransaction<TResult>(Func<OracleConnection, OracleTransaction, TResult> executeCommand)
        {
            using (var connection = new OracleConnection(_databaseConfiguration.ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    TResult result = executeCommand(connection, transaction);

                    transaction.Commit();

                    return result;
                }
            }
        }

        protected virtual void ExecuteTransaction(Action<OracleConnection, OracleTransaction> executeCommand)
        {
            using (var connection = new OracleConnection(_databaseConfiguration.ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    executeCommand(connection, transaction);

                    transaction.Commit();
                }
            }
        }
    }
}
