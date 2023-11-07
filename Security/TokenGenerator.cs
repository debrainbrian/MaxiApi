namespace MaxiApi.Security
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    internal class TokenGenerator
    {
        /// <summary>
        /// Get the configuration.
        /// </summary>
        private IConfiguration config;

        public TokenGenerator(IConfiguration configuration) 
        {
            config = configuration;
        }

        /// <summary>
        /// Generate a Jwt token.
        /// </summary>
        /// <param name="user">user name</param>
        /// <returns>The Jwt token</returns>
        public string GenerateToken(string user)
        {
            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]);
            var expires = int.Parse(config["Jwt:Expires"]);
            var signingCredentials = new SigningCredentials(
                                    new SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha512Signature
                                );

            var subject = new ClaimsIdentity(new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user),
            new Claim(JwtRegisteredClaimNames.Email, user),
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddMinutes(expires),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}