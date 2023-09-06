using greenharvestbox.Data.Resources;
using greenharvestbox.Data.Services.ConfigurationServices;
using greenharvestbox.Logic.Services.Jwt;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Test.ServiceTests
{
    public class JwtServiceTest
    {
        private readonly JwtService _jwtService;
        public JwtServiceTest()
        {
            _jwtService = new JwtService(new ConfigurationService());
        }

        [Theory]
        [InlineData(Roles.CUSTOMER)]
        [InlineData(Roles.VENDOR)]
        public void CreateToken_ShouldReturnToken_WhenRoleIsGiven(string role)
        {
            //Arrange
            //Create a token to test it
            string token = "";
            var tokenHandler = new JwtSecurityTokenHandler();

            //Act
            //Create handler to see what is inside of token (obtain a role that is given)
            token = _jwtService.CreateToken(role);          
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var roles = jwtToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();


            //Assert
            Assert.Equal(role, roles[0]);
            Assert.NotNull(token);
        }
    }
}
