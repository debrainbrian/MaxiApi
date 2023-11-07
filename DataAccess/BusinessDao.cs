namespace MaxiApi.DataAccess
{
    using MaxiApi.Models.Business;
    using System.Data.SqlClient;
    using System.Data;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    /// <summary>
    /// The business data access class.
    /// </summary>
    public class BusinessDao : ExecuteBase
    {
        /// <summary>
        /// Get the employee data.
        /// </summary>
        /// <returns>The employee data.</returns>
        public List<EmployeeData> GetEmployee(EmployeeData data)
        {
            var cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Bus].[GetEmployee]"
            };

            var pEmployeeId = cmd.CreateParameter();
            pEmployeeId.ParameterName = "@EmployeeId";
            pEmployeeId.DbType = DbType.Int32;
            pEmployeeId.Value = data.EmployeeId == -1 ? (object)DBNull.Value : data.EmployeeId;

            var pName = cmd.CreateParameter();
            pName.ParameterName = "@Name";
            pName.DbType = DbType.String;
            pName.Value = string.IsNullOrEmpty(data.Name) ? (object)DBNull.Value : data.Name;

            var pFirstName = cmd.CreateParameter();
            pFirstName.ParameterName = "@FirstName";
            pFirstName.DbType = DbType.String;
            pFirstName.Value = string.IsNullOrEmpty(data.FirstName) ? (object)DBNull.Value : data.FirstName;

            var pEmployeeNo = cmd.CreateParameter();
            pEmployeeNo.ParameterName = "@EmployeeNo";
            pEmployeeNo.DbType = DbType.String;
            pEmployeeNo.Value = string.IsNullOrEmpty(data.EmployeeNo) ? (object)DBNull.Value : data.EmployeeNo;

            cmd.Parameters.Add(pEmployeeId);
            cmd.Parameters.Add(pName);
            cmd.Parameters.Add(pFirstName);
            cmd.Parameters.Add(pEmployeeNo);

            var dt = this.ExecuteDataTableProcedure(cmd);

            if (dt.Rows.Count < 1)
            {
                return null;
            }

            return dt.Rows.Cast<DataRow>()
                .Select(
                    dr =>
                    new EmployeeData()
                    {
                        EmployeeId = int.Parse(dr["EmployeeId"].ToString()),
                        Name = dr["Name"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        BirthDate = DateTime.Parse(dr["BirthDate"].ToString()),
                        EmployeeNo = dr["EmployeeNo"].ToString(),
                        Curp = dr["Curp"].ToString(),
                        Ssn = dr["Ssn"].ToString(),
                        Telephone = dr["Telephone"].ToString(),
                        Nationality = dr["Nationality"].ToString(),
                    }).ToList();
        }

        /// <summary>
        /// Set the employee data.
        /// </summary>
        /// <param name="data">The employee data.</param>
        /// <returns>The employee id.</returns>
        public bool SetEmployee(EmployeeData data)
        {
            var result = false;
            var cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Bus].[SetEmployee]"
            };

            var pEmployeeId = cmd.CreateParameter();
            pEmployeeId.ParameterName = "@EmployeeId";
            pEmployeeId.DbType = DbType.Int32;
            pEmployeeId.Value = data.EmployeeId;

            var pName = cmd.CreateParameter();
            pName.ParameterName = "@Name";
            pName.DbType = DbType.String;
            pName.Value = data.Name;

            var pFirstName = cmd.CreateParameter();
            pFirstName.ParameterName = "@FirstName";
            pFirstName.DbType = DbType.String;
            pFirstName.Value = data.FirstName;

            var pLastName = cmd.CreateParameter();
            pLastName.ParameterName = "@LastName";
            pLastName.DbType = DbType.String;
            pLastName.Value = data.LastName;

            var pBirthDate = cmd.CreateParameter();
            pBirthDate.ParameterName = "@BirthDate";
            pBirthDate.DbType = DbType.Date;
            pBirthDate.Value = data.BirthDate;

            var pEmployeeNo = cmd.CreateParameter();
            pEmployeeNo.ParameterName = "@EmployeeNo";
            pEmployeeNo.DbType = DbType.String;
            pEmployeeNo.Value = data.EmployeeNo;

            var pCurp = cmd.CreateParameter();
            pCurp.ParameterName = "@Curp";
            pCurp.DbType = DbType.String;
            pCurp.Value = data.Curp;

            var pSsn = cmd.CreateParameter();
            pSsn.ParameterName = "@Ssn";
            pSsn.DbType = DbType.String;
            pSsn.Value = data.Ssn;

            var pTelephone = cmd.CreateParameter();
            pTelephone.ParameterName = "@Telephone";
            pTelephone.DbType = DbType.String;
            pTelephone.Value = data.Telephone;

            var pNationality = cmd.CreateParameter();
            pNationality.ParameterName = "@Nationality";
            pNationality.DbType = DbType.String;
            pNationality.Value = data.Nationality;

            cmd.Parameters.Add(pEmployeeId);
            cmd.Parameters.Add(pName);
            cmd.Parameters.Add(pFirstName);
            cmd.Parameters.Add(pLastName);
            cmd.Parameters.Add(pBirthDate);
            cmd.Parameters.Add(pEmployeeNo);
            cmd.Parameters.Add(pCurp);
            cmd.Parameters.Add(pSsn);
            cmd.Parameters.Add(pTelephone);
            cmd.Parameters.Add(pNationality);

            var id = this.ExecuteIntProcedure(cmd);

            if (id > 0)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Delete the employee.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <returns>If the employe was deleted.</returns>
        public bool DelEmployee(int employeeId)
        {
            var cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Bus].[DelEmployee]"
            };

            var pEmployeeId = cmd.CreateParameter();
            pEmployeeId.ParameterName = "@EmployeeId";
            pEmployeeId.DbType = DbType.Int32;
            pEmployeeId.Value = employeeId;

            cmd.Parameters.Add(pEmployeeId);

            this.ExecuteCommand(cmd);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<BeneficiaryData> GetBeneficiary(int employeeId)
        {
            var cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Bus].[GetBeneficiary]"
            };

            var pEmployeeId = cmd.CreateParameter();
            pEmployeeId.ParameterName = "@EmployeeId";
            pEmployeeId.DbType = DbType.Int32;
            pEmployeeId.Value = employeeId;

            cmd.Parameters.Add(pEmployeeId);

            var dt = this.ExecuteDataTableProcedure(cmd);

            if (dt.Rows.Count < 1)
            {
                return null;
            }

            return dt.Rows.Cast<DataRow>()
                .Select(
                    dr =>
                    new BeneficiaryData()
                    {
                        BeneficiaryId = int.Parse(dr["BeneficiaryId"].ToString()),
                        EmployeeId = int.Parse(dr["EmployeeId"].ToString()),
                        Name = dr["Name"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        BirthDate = DateTime.Parse(dr["BirthDate"].ToString()),
                        Curp = dr["Curp"].ToString(),
                        Ssn = dr["Ssn"].ToString(),
                        Telephone = dr["Telephone"].ToString(),
                        Nationality = dr["Nationality"].ToString(),
                        Percent = int.Parse(dr["Percent"].ToString()),
                    }).ToList();
        }

        /// <summary>
        /// Set the beneficiary data.
        /// </summary>
        /// <param name="data">The beneficiary data.</param>
        /// <returns>If the employe was saved.</returns>
        public List<BeneficiaryData> SetBeneficiary(BeneficiaryData data)
        {
            var employeeId = data.EmployeeId;

            var cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Bus].[SetBeneficiary]"
            };

            var pBeneficiaryId = cmd.CreateParameter();
            pBeneficiaryId.ParameterName = "@BeneficiaryId";
            pBeneficiaryId.DbType = DbType.Int32;
            pBeneficiaryId.Value = data.BeneficiaryId;

            var pEmployeeId = cmd.CreateParameter();
            pEmployeeId.ParameterName = "@EmployeeId";
            pEmployeeId.DbType = DbType.Int32;
            pEmployeeId.Value = data.EmployeeId;

            var pName = cmd.CreateParameter();
            pName.ParameterName = "@Name";
            pName.DbType = DbType.String;
            pName.Value = data.Name;

            var pFirstName = cmd.CreateParameter();
            pFirstName.ParameterName = "@FirstName";
            pFirstName.DbType = DbType.String;
            pFirstName.Value = data.FirstName;

            var pLastName = cmd.CreateParameter();
            pLastName.ParameterName = "@LastName";
            pLastName.DbType = DbType.String;
            pLastName.Value = data.LastName;

            var pBirthDate = cmd.CreateParameter();
            pBirthDate.ParameterName = "@BirthDate";
            pBirthDate.DbType = DbType.Date;
            pBirthDate.Value = data.BirthDate;

            var pCurp = cmd.CreateParameter();
            pCurp.ParameterName = "@Curp";
            pCurp.DbType = DbType.String;
            pCurp.Value = data.Curp;

            var pSsn = cmd.CreateParameter();
            pSsn.ParameterName = "@Ssn";
            pSsn.DbType = DbType.String;
            pSsn.Value = data.Ssn;

            var pTelephone = cmd.CreateParameter();
            pTelephone.ParameterName = "@Telephone";
            pTelephone.DbType = DbType.String;
            pTelephone.Value = data.Telephone;

            var pNationality = cmd.CreateParameter();
            pNationality.ParameterName = "@Nationality";
            pNationality.DbType = DbType.String;
            pNationality.Value = data.Nationality;

            var pPercent = cmd.CreateParameter();
            pPercent.ParameterName = "@Percent";
            pPercent.DbType = DbType.Int32;
            pPercent.Value = data.Percent;

            cmd.Parameters.Add(pBeneficiaryId);
            cmd.Parameters.Add(pEmployeeId);
            cmd.Parameters.Add(pName);
            cmd.Parameters.Add(pFirstName);
            cmd.Parameters.Add(pLastName);
            cmd.Parameters.Add(pBirthDate);
            cmd.Parameters.Add(pCurp);
            cmd.Parameters.Add(pSsn);
            cmd.Parameters.Add(pTelephone);
            cmd.Parameters.Add(pNationality);
            cmd.Parameters.Add(pPercent);

            var id = this.ExecuteIntProcedure(cmd);

            return this.GetBeneficiary(employeeId);
        }

        /// <summary>
        /// Delete the beneficiaries
        /// </summary>
        /// <param name="employeeId"></param>
        public void DelBeneficiary(int employeeId)
        {
            var cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Bus].[DelBeneficiary]"
            };

            var pBeneficiary = cmd.CreateParameter();
            pBeneficiary.ParameterName = "@BeneficiaryId";
            pBeneficiary.DbType = DbType.Int32;
            pBeneficiary.Value = employeeId;

            cmd.Parameters.Add(pBeneficiary);

            this.ExecuteCommand(cmd);
        }
    }
}