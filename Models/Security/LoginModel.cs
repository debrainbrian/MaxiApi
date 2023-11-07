namespace MaxiApi.Models.Security
{
    public class LoginModel
    {
        /// <summary>
        /// The login user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The login password.
        /// </summary>
        public string Password { get; set; }
    }
}