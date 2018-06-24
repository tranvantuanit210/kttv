using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace KTTV.Entities.Utilities
{
    /// <summary>
    /// Static class providing data access functionalities using ADO.NET
    /// </summary>
    public static class SqlHelper
    {
        private static readonly string Connection = System.Configuration.ConfigurationManager.ConnectionStrings["KTTVDBConnection"].ConnectionString;

        /// <summary>
        /// Begin a sql transaction.
        /// </summary>
        public static SqlTransaction BeginTransaction(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                connectionString = Connection;

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            var sqlTransaction = sqlConnection.BeginTransaction();

            return sqlTransaction;
        }

        /// <summary>
        /// Transaction commit.
        /// </summary>
        public static void TransactionCommit(SqlTransaction transaction)
        {
            transaction.Commit();
            if (transaction.Connection != null)
                transaction.Connection.Close();
        }

        /// <summary>
        /// Transaction rollback.
        /// </summary>
        public static void TransactionRollback(SqlTransaction transaction)
        {

            if (transaction.Connection != null)
            {
                transaction.Rollback();
                transaction.Connection.Close();
            }
                
        }

        /// <summary>
        /// Creates a sql command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="transaction">The SQL transaction.</param>
        /// <returns>
        /// Sql command created.
        /// </returns>
        public static SqlCommand CreateCommand(string commandText, CommandType commandType, SqlTransaction transaction = null)
        {
            var cmd = new SqlCommand
            {
                CommandText = commandText,
                CommandType = commandType,
                CommandTimeout = 60
            };

            if (transaction != null)
                cmd.Transaction = transaction;

            return cmd;
        }

        /// <summary>
        /// Execute the SqlCommand and fill the data queried to a table
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>
        /// A data table.
        /// </returns>
        public static DataTable QueryDataTable(SqlCommand command, string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                connectionString = Connection;

            if (string.IsNullOrEmpty(connectionString))
                return new DataTable();

            using (var connection = new SqlConnection { ConnectionString = connectionString })
            {
                using (var da = new SqlDataAdapter(command))
                {
                    connection.Open();
                    command.Connection = connection;
                    var dataSet = new DataSet();
                    da.Fill(dataSet);
                    connection.Close();

                    var dt = dataSet.Tables[0];
                    dataSet.Dispose();

                    return dt;
                }
            }
        }

        public static IList<DataTable> QueryMultipleDataTables(SqlCommand command, string connectionString = null)
        {
            var dataTables = new List<DataTable>();
            if (string.IsNullOrEmpty(connectionString))
                connectionString = Connection;

            if (string.IsNullOrEmpty(connectionString))
                return dataTables;

            using (var connection = new SqlConnection { ConnectionString = connectionString })
            {
                using (var da = new SqlDataAdapter(command))
                {
                    connection.Open();
                    command.Connection = connection;
                    var dataSet = new DataSet();
                    da.Fill(dataSet);
                    connection.Close();
                    dataTables.AddRange(dataSet.Tables.Cast<DataTable>());
                    dataSet.Dispose();

                    return dataTables;
                }
            }
        }

        public static DataSet QueryDataSet(SqlCommand command, string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                connectionString = Connection;

            if (string.IsNullOrEmpty(connectionString))
                return new DataSet();

            using (var connection = new SqlConnection { ConnectionString = connectionString })
            {
                using (var da = new SqlDataAdapter(command))
                {
                    connection.Open();
                    command.Connection = connection;
                    var dataSet = new DataSet();
                    da.Fill(dataSet);
                    connection.Close();

                    return dataSet;
                }
            }
        }

        /// <summary>
        /// Define the SqlCommand then Execute it and fill the data queried to a table
        /// </summary>
        /// <param name="sqlQuery">The SQL query.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>
        /// A data table.
        /// </returns>
        public static DataTable QueryDataTable(string sqlQuery, CommandType commandType, string connectionString = null)
        {
            return QueryDataTable(CreateCommand(sqlQuery, commandType), connectionString);
        }

        /// <summary>
        /// Executes non query with SqlCommand.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>Execution status</returns>
        public static int ExecuteNonQuery(SqlCommand command, string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                connectionString = Connection;

            using (var connection = new SqlConnection { ConnectionString = connectionString })
            {
                connection.Open();
                command.Connection = connection;
                var result = command.ExecuteNonQuery();
                connection.Close();

                return result;
            }
        }

        /// <summary>
        /// Executes non query in side a transaction.
        /// </summary>
        /// <param name="command">The SQL command.</param>
        /// <param name="connection">The SQL connection.</param>
        /// <returns>Execution status</returns>
        public static int ExecuteNonQueryWithTransaction(SqlCommand command, SqlConnection connection)
        {
            command.Connection = connection;
            var result = command.ExecuteNonQuery();

            return result;
        }

        /// <summary>
        /// Executes non query with sql query string.
        /// </summary>
        /// <param name="queryString">The query string.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>Execution status</returns>
        public static int ExecuteNonQuery(string queryString, string connectionString = null)
        {
            return ExecuteNonQuery(new SqlCommand(queryString), connectionString);
        }

        /// <summary>
        /// Executes scalar with SqlCommand.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        public static object ExecuteScalar(SqlCommand command, string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                connectionString = Connection;

            using (var connection = new SqlConnection { ConnectionString = connectionString })
            {
                connection.Open();
                command.Connection = connection;
                var result = command.ExecuteScalar();
                connection.Close();

                return result;
            }
        }

        /// <summary>
        /// Executes scalar with Sql query string.
        /// </summary>
        /// <param name="queryString">The query string.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        public static object ExecuteScalar(string queryString, string connectionString = null)
        {
            return ExecuteScalar(new SqlCommand(queryString), connectionString);
        }

        /// <summary>
        /// Executes scalar in side a transaction.
        /// </summary>
        /// <param name="command">The SQL command.</param>
        /// <param name="connection">The SQL connection.</param>
        /// <returns></returns>
        public static object ExecuteScalarWithTransaction(SqlCommand command, SqlConnection connection)
        {
            command.Connection = connection;
            var result = command.ExecuteScalar();

            return result;
        }

        /// <summary>
        /// Creates the in parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <param name="sqlDbType">Type of the SQL database.</param>
        /// <param name="size">The size.</param>
        /// <returns>
        /// Sql parameter.
        /// </returns>
        public static SqlParameter CreateInParameter(string parameterName, object value,
            SqlDbType sqlDbType = SqlDbType.NVarChar, int size = 0)
        {
            if (size > 0)
            {
                return new SqlParameter
                {
                    ParameterName = parameterName,
                    SqlDbType = sqlDbType,
                    Direction = ParameterDirection.Input,
                    Value = value ?? DBNull.Value,
                    Size = size
                };
            }

            return new SqlParameter
            {
                ParameterName = parameterName,
                SqlDbType = sqlDbType,
                Direction = ParameterDirection.Input,
                Value = value ?? DBNull.Value
            };
        }

        /// <summary>
        /// Creates the out parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="sqlDbType">Type of the SQL database.</param>
        /// <returns>
        /// Sql parameter.
        /// </returns>
        public static SqlParameter CreateOutParameter(string parameterName, SqlDbType sqlDbType = SqlDbType.Int)
        {
            return new SqlParameter
            {
                ParameterName = parameterName,
                SqlDbType = sqlDbType,
                Direction = ParameterDirection.Output,
                Size = Int32.MaxValue
            };
        }

        /// <summary>
        /// Creates the in out parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <param name="sqlDbType">Type of the SQL database.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static SqlParameter CreateInOutParameter(string parameterName, object value,
            SqlDbType sqlDbType = SqlDbType.VarChar, int size = 0)
        {
            if (size > 0)
            {
                return new SqlParameter
                {
                    ParameterName = parameterName,
                    SqlDbType = sqlDbType,
                    Direction = ParameterDirection.InputOutput,
                    Value = value ?? DBNull.Value,
                    Size = size
                };
            }

            return new SqlParameter
            {
                ParameterName = parameterName,
                SqlDbType = sqlDbType,
                Direction = ParameterDirection.InputOutput,
                Value = value ?? DBNull.Value
            };
        }
    }
}
