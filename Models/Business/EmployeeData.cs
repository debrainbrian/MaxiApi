namespace MaxiApi.Models.Business
{
    /// <summary>
    /// The employee data model.
    /// </summary>
    public class EmployeeData
    {
        /// <summary>
        /// The employee id.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// The name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The birth date.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// The employee number.
        /// </summary>
        public string EmployeeNo { get; set; }

        /// <summary>
        /// The CURP.
        /// </summary>
        public string Curp { get; set; }

        /// <summary>
        /// The ssn.
        /// </summary>
        public string Ssn { get; set; }

        /// <summary>
        /// The telephone.
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// The nationality.
        /// </summary>
        public string Nationality { get; set; }
    }
}