using greenharvestbox.Data.Resources;
using greenharvestbox.Data.Services.ConfigurationServices;
using greenharvestbox.Logic.Services.Cipher;
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
    public class CipherServiceTest
    {
        private readonly CipherService _cipherService;
        public CipherServiceTest()
        {
            _cipherService = new CipherService();
        }

        [Fact]
        public void CreatePasswordHash_ShouldReturnPasswordHashAndSalt_WhenPasswordIsValid()
        {
            // Arrange
            string password = "123456"; 
            byte[] passwordHash;
            byte[] passwordSalt;


            // Act
            _cipherService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Assert
            Assert.NotNull(passwordHash); 
            Assert.NotNull(passwordSalt); 
        }
        [Fact]
        public void CreatePasswordHash_ShouldReturnNull_WhenPasswordIsNull()
        {
            // Arrange
            string? password = null; 
            byte[] passwordHash;
            byte[] passwordSalt;


            // Act
            _cipherService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Assert
            Assert.Null(passwordHash); 
            Assert.Null(passwordSalt); 
        }
    }
}
