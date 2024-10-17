using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Halda.Application.Services
{
    public class TokenService : ITokenService
    {
        public string name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ReadToken(string idtoken)
        {
            var token = new JwtSecurityToken(jwtEncodedString: idtoken);

            //string UserEmail = token.Claims.First(c => c.Type == name).Value;
        }


        public string ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    //IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userEmail = jwtToken.Claims.First(x => x.Type == name).Value;

                // return user id from JWT token if validation successful
                return userEmail;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
