namespace MaxiApi.DataAccess
{
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// The execute sql base.
    /// </summary>
    public class ExecuteBase
    {
        /// <summary>
        /// Exists a transaction.
        /// </summary>
        protected bool hasTransaction = false;

        /// <summary>
        /// SQL connection.
        /// </summary>
        protected SqlConnection connection = null;

        /// <summary>
        /// SQL transaction.
        /// </summary>
        protected SqlTransaction transaction = null;

        #region Protected Methods

        /// <summary>
        /// Method to execute a stored procedure that returns an string value.
        /// </summary>
        /// <param name="cmd">SQL command</param>
        /// <returns>String result</returns>
        protected string ExecuteStringProcedure(SqlCommand cmd)
        {
            object result;

            if (hasTransaction)
            {
                cmd.Connection = this.connection;
                cmd.Transaction = this.transaction;
                result = cmd.ExecuteScalar();
            }
            else
            {
                using (var conn = new SqlConnection(this.GetConnectionString()))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    result = cmd.ExecuteScalar();
                    conn.Close();
                }
            }

            return result == null ? string.Empty : result.ToString();
        }

        /// <summary>
        /// Method to execute a stored procedure that returns an int value.
        /// </summary>
        /// <param name="cmd">SQL command</param>
        /// <returns>Int result</returns>
        protected int ExecuteIntProcedure(SqlCommand cmd)
        {
            object result;

            if (hasTransaction)
            {
                cmd.Connection = this.connection;
                cmd.Transaction = this.transaction;
                result = cmd.ExecuteScalar();
            }
            else
            {
                using (var conn = new SqlConnection(this.GetConnectionString()))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    result = cmd.ExecuteScalar();
                    conn.Close();
                }
            }

            return int.Parse(result.ToString());
        }

        /// <summary>
        /// Method to execute a stored procedure that returns a data reader.
        /// </summary>
        /// <param name="cmd">SQL command</param>
        /// <returns>Data reader</returns>
        protected SqlDataReader ExecuteReaderProcedure(SqlCommand cmd)
        {
            SqlDataReader result;

            if (hasTransaction)
            {
                cmd.Connection = this.connection;
                cmd.Transaction = this.transaction;
                result = cmd.ExecuteReader();
            }
            else
            {
                using (var conn = new SqlConnection(this.GetConnectionString()))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    result = cmd.ExecuteReader();
                    conn.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// Method to execute a stored procedure that returns a data table.
        /// </summary>
        /// <param name="cmd">SQL command</param>
        /// <returns>Data table</returns>
        protected DataTable ExecuteDataTableProcedure(SqlCommand cmd)
        {
            SqlDataReader result;
            var dt = new DataTable();

            if (hasTransaction)
            {
                cmd.Connection = this.connection;
                cmd.Transaction = this.transaction;
                result = cmd.ExecuteReader();
                dt.Load(result);
            }
            else
            {
                using (var conn = new SqlConnection(this.GetConnectionString()))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    result = cmd.ExecuteReader();
                    dt.Load(result);
                    conn.Close();
                }
            }

            return dt;
        }

        /// <summary>
        /// The execute command.
        /// </summary>
        /// <param name="cmd">
        /// The cmd.
        /// </param>
        protected void ExecuteCommand(SqlCommand cmd)
        {
            if (hasTransaction)
            {
                cmd.Connection = this.connection;
                cmd.Transaction = this.transaction;
                cmd.ExecuteNonQuery();
            }
            else
            {
                using (var conn = new SqlConnection(this.GetConnectionString()))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Begin a SQL transaction.
        /// </summary>
        protected void BeginTransaction()
        {
            this.hasTransaction = true;
            this.connection = new SqlConnection(this.GetConnectionString());
            this.connection.Open();
            this.transaction = this.connection.BeginTransaction();
        }

        /// <summary>
        /// Commit a SQL transaction.
        /// </summary>
        protected void CommitTransaction()
        {
            this.hasTransaction = false;
            this.transaction.Commit();
            this.connection.Close();
        }

        /// <summary>
        /// Rollback a SQL transaction.
        /// </summary>
        protected void RollbackTransaction()
        {
            this.hasTransaction = false;
            this.transaction.Rollback();
            this.connection.Close();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the connection string
        /// </summary>
        /// <returns>Connection String</returns>
        private string GetConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            return configuration.GetConnectionString("DefaultConnection");
        }

        #endregion
    }
}