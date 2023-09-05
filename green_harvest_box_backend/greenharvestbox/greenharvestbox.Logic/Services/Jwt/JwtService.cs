
using greenharvestbox.Data.Services.ConfigurationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace greenharvestbox.Logic.Services.Jwt
{
    public class JwtService : IJwtService
    {

        private readonly IConfigurationService _configurationService;


        public JwtService(IConfigurationService configurationService)

        {
            _configurationService = configurationService;
        }
        public string CreateToken(string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,role),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configurationService.GetMySecretKey()));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                audience: "aa",
                issuer: "bb",
                claims: claims,
                expires: DateTime.Now.AddMonths(9),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
      


        /*
        public bool validateToken(string token)
        {
            try
            {
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configurationService.GetMySecretKey()));
                JwtSecurityTokenHandler handler = new();
                handler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false
                }, out SecurityToken validatedToken);
                //var jwtToken = (JwtSecurityToken)validatedToken;
                //var claims = jwtToken.Claims.ToList();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public int GetUserIdFromToken(IHeaderDictionary headers)
        {
            string requestToken = headers[HeaderNames.Authorization].ToString().Replace("bearer ", "");
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(requestToken);
            string user = jwt.Claims.First(c => c.Type == "id").Value;
            int userId = int.Parse(user);
            return userId;
        }
        public string GetUserRoleFromToken(IHeaderDictionary headers)
        {
            string requestToken = headers[HeaderNames.Authorization].ToString().Replace("bearer ", "");
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(requestToken);
            string role = jwt.Claims.First(c => c.Type == "role").Value;
            return role;
        }
        public string GetUserNameFromToken(IHeaderDictionary headers)
        {
            string requestToken = headers[HeaderNames.Authorization].ToString().Replace("bearer ", "");
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(requestToken);
            string role = jwt.Claims.First(c => c.Type == "username").Value;
            return role;
        }
        public string GetUserTokenFromToken(IHeaderDictionary headers)
        {
            string requestToken = headers[HeaderNames.Authorization].ToString().Replace("bearer ", "");
            return requestToken;
        }
        
        public PassengerVerifying GetUserInformation(IHeaderDictionary headers)
        {
            PassengerVerifying userVerifyingDto = new PassengerVerifying()
            {
                Id = GetUserIdFromToken(headers),
                Token = GetUserTokenFromToken(headers)
            };
            return userVerifyingDto;
        }
        */
    }
}
