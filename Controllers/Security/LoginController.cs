namespace MaxiApi.Controllers.Security
{
    using System.Reflection;

    using MaxiApi.Models.Security;
    using MaxiApi.Security;
    using MaxiApi.Error;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using MaxiApi.DataAccess;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        IConfiguration configuration;

        /// <summary>
        /// The cotroller initialize.
        /// </summary>
        /// <param name="configuration">the configuration.</param>
        public LoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        /// <summary>
        /// The authenticate method.
        /// </summary>
        /// <param name="value">The login model.</param>
        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate(LoginModel data)
        {
            try
            {
                UserData result = null;
                var security = new SecurityDao();
                var user = security.GetUser(-1, data.UserName).FirstOrDefault();

                string pass = configuration["Jwt:Key"].ToString();
                var isValid = data.Password == Crypto.Decrypt(user.Password, pass);

                if (isValid)
                {
                    var tokenGenerator = new TokenGenerator(configuration);
                    var token = tokenGenerator.GenerateToken(user.Nickname);
                    security.SetUserLogin(user.UserId, token);
                    result = security.GetUser(user.UserId, null).FirstOrDefault();

                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                var error = new Log();
                var id = error.LogError(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex).ToString();
                return BadRequest(id);
            }
        }
    }
}
