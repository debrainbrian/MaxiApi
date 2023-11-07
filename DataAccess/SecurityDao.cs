namespace MaxiApi.DataAccess
{
    using System.Data.SqlClient;
    using System.Data;
    using MaxiApi.Models.Security;

    /// <summary>
    /// The security data access class.
    /// </summary>
    public class SecurityDao : ExecuteBase
    {
        #region Login

        /// <summary>
        /// Get the user.
        /// </summary>
        /// <returns>The user list.</returns>
        public List<UserData> GetUser(int userId, string nickname)
        {
            var cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Sec].[GetUser]"
            };

            var pUserId = cmd.CreateParameter();
            pUserId.ParameterName = "@UserId";
            pUserId.DbType = DbType.Int32;
            pUserId.Value = userId == -1 ? (object)DBNull.Value : userId;

            var pNickname = cmd.CreateParameter();
            pNickname.ParameterName = "@Nickname";
            pNickname.DbType = DbType.String;
            pNickname.Value = nickname ?? (object)DBNull.Value;

            var pIsActive = cmd.CreateParameter();
            pIsActive.ParameterName = "@IsActive";
            pIsActive.DbType = DbType.Boolean;
            pIsActive.Value = true;

            cmd.Parameters.Add(pUserId);
            cmd.Parameters.Add(pNickname);
            cmd.Parameters.Add(pIsActive);

            var dt = this.ExecuteDataTableProcedure(cmd);

            if (dt.Rows.Count < 1)
            {
                return new List<UserData>();
            }

            return dt.Rows.Cast<DataRow>()
                .Select(
                    dr =>
                    new UserData()
                    {
                        UserId = int.Parse(dr["UserId"].ToString()),
                        RoleId = int.Parse(dr["RoleId"].ToString()),
                        Nickname = dr["Nickname"].ToString(),
                        Password = dr["Password"].ToString(),
                        Name = dr["Name"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Email = dr["Email"].ToString(),
                        Token = dr["LastHash"].ToString(),
                        IsActive = bool.Parse(dr["IsActive"].ToString()),
                    }).ToList();
        }

        /// <summary>
        /// Set the user login data.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="token">The user token.</param>
        public void SetUserLogin(int userId, string token)
        {
            var cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Sec].[SetLoginData]"
            };

            var pUserId = cmd.CreateParameter();
            pUserId.ParameterName = "@UserId";
            pUserId.DbType = DbType.Int32;
            pUserId.Value = userId;



            var pToken = cmd.CreateParameter();
            pToken.ParameterName = "@Token";
            pToken.DbType = DbType.String;
            pToken.Value = token;

            var pLastLoggedIn = cmd.CreateParameter();
            pLastLoggedIn.ParameterName = "@LastLogin";
            pLastLoggedIn.DbType = DbType.DateTime;
            pLastLoggedIn.Value = DateTime.Now;

            cmd.Parameters.Add(pUserId);
            cmd.Parameters.Add(pToken);
            cmd.Parameters.Add(pLastLoggedIn);

            this.ExecuteCommand(cmd);
        }

        #endregion
    }
}