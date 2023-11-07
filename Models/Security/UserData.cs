namespace MaxiApi.Models.Security
{
    /// <summary>
    /// The user data.
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// The user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The role id.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// The user nickname.
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// The user password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The user name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The User Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The User Is Active.
        /// </summary>
        public bool IsActive { get; set; }
    }
}