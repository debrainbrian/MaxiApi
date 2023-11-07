namespace MaxiApi.DataAccess
{
    using System.Data.SqlClient;
    using System.Data;

    /// <summary>
    /// The error log class.
    /// </summary>
    public class Errorlog : ExecuteBase
    {
        /// <summary>
        /// Log the error.
        /// </summary>
        /// <param name="sClass">The class name</param>
        /// <param name="sMethod">The method name.</param>
        /// <param name="ex">The exception.</param>
        /// <returns>The error id.</returns>
        public int LogError(string sClass, string sMethod, Exception ex)
        {
            var cmd = new SqlCommand();
            var pErrorDate = cmd.CreateParameter();
            var pClass = cmd.CreateParameter();
            var pMethod = cmd.CreateParameter();
            var pTrace = cmd.CreateParameter();
            var pMessage = cmd.CreateParameter();
            var pSource = cmd.CreateParameter();

            pErrorDate.ParameterName = "@ErrorDate";
            pErrorDate.DbType = DbType.DateTime;
            pErrorDate.Value = DateTime.Now;
            pClass.ParameterName = "@Class";
            pClass.DbType = DbType.String;
            pClass.Value = sClass;
            pMethod.ParameterName = "@Method";
            pMethod.DbType = DbType.String;
            pMethod.Value = sMethod;
            pTrace.ParameterName = "@Trace";
            pTrace.DbType = DbType.String;
            pTrace.Value = ex.StackTrace == null ? "" : ex.StackTrace;
            pMessage.ParameterName = "@Message";
            pMessage.DbType = DbType.String;
            pMessage.Value = ex.Message == null ? "" : ex.Message;
            pSource.ParameterName = "@Source";
            pSource.DbType = DbType.String;
            pSource.Value = ex.InnerException == null ? "" : ex.InnerException.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Err].[SaveError]";
            cmd.Parameters.Add(pErrorDate);
            cmd.Parameters.Add(pClass);
            cmd.Parameters.Add(pMethod);
            cmd.Parameters.Add(pTrace);
            cmd.Parameters.Add(pMessage);
            cmd.Parameters.Add(pSource);

            return this.ExecuteIntProcedure(cmd); ;
        }
    }
}