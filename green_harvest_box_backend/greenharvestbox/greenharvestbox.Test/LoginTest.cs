using AutoMapper;
using Castle.Core.Resource;
using greenharvestbox.Data.Models;
using greenharvestbox.Data.Repositories.LoginRepository;
using greenharvestbox.Data.Resources;
using greenharvestbox.Data.Resources.Messages;
using greenharvestbox.Data.Services.ConfigurationServices;
using greenharvestbox.Logic.Models.dto;
using greenharvestbox.Logic.Models.dto.Login.dto;
using greenharvestbox.Logic.Services.Cipher;
using greenharvestbox.Logic.Services.Jwt;
using greenharvestbox.Logic.Services.Mapper;
using greenharvestbox.Logic.Services.UserServices.LoginService;
using greenharvestbox.Logic.Services.UtilityServices;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Moq;
using System.Data.Common;

namespace greenharvestbox.Test
{
    public class LoginTest
    {
 
        private readonly ILoginService _loginService;
        private readonly IConfigurationService _configurationService;
        private readonly LoginRepository _loginRepository;
        private readonly CipherService _cipherService;
        private readonly ILogger<LoginService> _logger;
        private readonly IJwtService _jwtService;
        private readonly UtilityService _utilityService;
        private readonly IMapper _mapper;
        private readonly Mock<IConfigurationService> _configurationServiceMock = new Mock<IConfigurationService>();
        private readonly Mock<ICipherService> _cipherServiceMock = new Mock<ICipherService>();
        private readonly Mock<IJwtService> _jwtServiceMock = new Mock<IJwtService>();
        private readonly Mock<ILogger<LoginService>> _loggerMock = new Mock<ILogger<LoginService>>();
        private readonly Mock<ILoginRepository> _loginRepositoryMock = new Mock<ILoginRepository>();
        private readonly Mock<IUtilityService> _utilityServiceMock = new Mock<IUtilityService>();

        public LoginTest()
        {
            _loginRepository = new LoginRepository(new ConfigurationService());
            _cipherService = new CipherService();
            _utilityService = new UtilityService();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperService>();
            });
            _mapper = config.CreateMapper();
            _loginService = new LoginService(_cipherServiceMock.Object, _jwtServiceMock.Object, _loginRepositoryMock.Object, _loggerMock.Object, _utilityService);

            //_jwtServiceMock = new JwtService(_configurationServiceMock.Object);
        }
        [Fact]
        public void Register_ShouldReturnSuccessResponse_WhenParametersAreValid()
        {
            //ARRANGE
            string token = "test_token";
            string password = "123456";
            _cipherService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            byte[] passHash = passwordHash;
            byte[] passSalt = passwordSalt;

            UserRegisterDto userRegisterDto = new UserRegisterDto()
            {
                Name = "emre",
                Surname = "yýlmaz",
                Email = "emreyilmaz@hotmail.com",
                PhoneNumber = "5412147452",
                Password = password,
                City = "izmir",
                Province = "bornova",
                IsCompanyWorker = false
            };
            User user = _mapper.Map<User>(userRegisterDto);
            user.Id = 1;
            user.RegisteredAt = DateTime.Now;
            user.PasswordHash = passHash;
            user.PasswordSalt = passSalt;
            user.Token = token;

            _jwtServiceMock.Setup(x => x.CreateToken(Roles.CUSTOMER)).Returns(token);
            _cipherServiceMock.Setup(x => x.CreatePasswordHash(It.IsAny<string>(), out It.Ref<byte[]>.IsAny, out It.Ref<byte[]>.IsAny))
             .Callback((string password, out byte[] passwordHash, out byte[] passwordSalt) =>
             {
                 passwordHash = passHash;
                 passwordSalt = passSalt;
             });
            _loginRepositoryMock.Setup(x => x.AddUser(It.IsAny<User>())).Returns(user);

            //ACT
            Response<UserBasicDto> response = _loginService.Register(userRegisterDto);

            //ASSERT

            Assert.Equal(Success.USER_ADDED, response.Message);
            Assert.NotNull(response.Data);
            Assert.True(response.Progress);

            Assert.Equal(user.Id, response.Data.Id);
            Assert.Equal(user.Name, response.Data.Name);
            Assert.Equal(user.Surname, response.Data.Surname);
            Assert.Equal(user.Email, response.Data.Email);
            Assert.Equal(user.PhoneNumber, response.Data.PhoneNumber);
            Assert.Equal(user.Address, response.Data.Address);
            Assert.Equal(user.City, response.Data.City);
            Assert.Equal(user.Province, response.Data.Province);
            Assert.Equal(user.Token, response.Data.Token);

        }
        [Fact]
        public void Register_ShouldReturnErrorResponse_WhenUserNotAdded()
        {
            //ARRANGE
            string token = "test_token";
            string password = "123456";
            _cipherService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            byte[] passHash = passwordHash;
            byte[] passSalt = passwordSalt;

            UserRegisterDto userRegisterDto = new UserRegisterDto()
            {
                Name = "emre",
                Surname = "yýlmaz",
                Email = "emreyilmaz@hotmail.com",
                PhoneNumber = "5412147452",
                Password = password,
                City = "izmir",
                Province = "bornova",
                IsCompanyWorker = false
            };

            User user = _mapper.Map<User>(userRegisterDto);
            user.Id = 1;
            user.RegisteredAt = DateTime.Now;
            user.PasswordHash = passHash;
            user.PasswordSalt = passSalt;
            user.Token = token;

            User nullUser = null;

            _jwtServiceMock.Setup(x => x.CreateToken(Roles.CUSTOMER)).Returns(token);
            _cipherServiceMock.Setup(x => x.CreatePasswordHash(It.IsAny<string>(), out It.Ref<byte[]>.IsAny, out It.Ref<byte[]>.IsAny))
               .Callback((string password, out byte[] passwordHash, out byte[] passwordSalt) =>
               {
                   passwordHash = passHash;
                   passwordSalt = passSalt;
               });           
            _loginRepositoryMock.Setup(x => x.AddUser(It.IsAny<User>())).Returns(nullUser);

            //ACT
            Response<UserBasicDto> response = _loginService.Register(userRegisterDto);

            //ASSERT
            Assert.Equal(Error.USER_NOT_ADDED, response.Message);
            Assert.True(response.Data.Id == 0);
            Assert.False(response.Progress);
        }
        [Fact]
        public void Register_ShouldReturnErrorResponse_WhenTokenIsNull()
        {
            //ARRANGE
            string? token = null;
            string password = "123456";
            _cipherService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            byte[] passHash = passwordHash;
            byte[] passSalt = passwordSalt;

            UserRegisterDto userRegisterDto = new UserRegisterDto()
            {
                Name = "emre",
                Surname = "yýlmaz",
                Email = "emreyilmaz@hotmail.com",
                PhoneNumber = "5412147452",
                Password = password,
                City = "izmir",
                Province = "bornova",
                IsCompanyWorker = false
            };


            _jwtServiceMock.Setup(x => x.CreateToken(Roles.CUSTOMER)).Returns(token);
            _cipherServiceMock.Setup(x => x.CreatePasswordHash(It.IsAny<string>(), out It.Ref<byte[]>.IsAny, out It.Ref<byte[]>.IsAny))
             .Callback((string password, out byte[] passwordHash, out byte[] passwordSalt) =>
             {
                 passwordHash = passHash;
                 passwordSalt = passSalt;
             });
            //ACT
            Response<UserBasicDto> response = _loginService.Register(userRegisterDto);
            //ASSERT
      
            Assert.Equal(_utilityService.CombineStrings(Error.TOKEN_NOT_CREATED,Error.USER_NOT_ADDED), response.Message);
            Assert.True(response.Data.Id == 0);
            Assert.False(response.Progress);
        }
        [Fact]
        public void Register_ShouldReturnErrorResponse_WhenPasswordAreInvalid()
        {
            //ARRANGE
            string password = "";
            _cipherService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            byte[] passHash = passwordHash;
            byte[] passSalt = passwordSalt;

            UserRegisterDto userRegisterDto = new UserRegisterDto()
            {
                Name = "emre",
                Surname = "yýlmaz",
                Email = "emreyilmaz@hotmail.com",
                PhoneNumber = "5412147452",
                Password = password,
                City = "izmir",
                Province = "bornova",
                IsCompanyWorker = false
            };

            _cipherServiceMock.Setup(x => x.CreatePasswordHash(It.IsAny<string>(), out It.Ref<byte[]>.IsAny, out It.Ref<byte[]>.IsAny))
                .Callback((string password, out byte[] passwordHash, out byte[] passwordSalt) =>
                {
                    passwordHash = null; 
                    passwordSalt = null; 
                });

            //ACT
            Response<UserBasicDto> response = _loginService.Register(userRegisterDto);

            //ASSERT
            Assert.Equal(_utilityService.CombineStrings(Error.PASSWORD_NOT_ENCRYPTED, Error.USER_NOT_ADDED), response.Message);
            Assert.True(response.Data.Id == 0);
            Assert.False(response.Progress);

        }

    }
}