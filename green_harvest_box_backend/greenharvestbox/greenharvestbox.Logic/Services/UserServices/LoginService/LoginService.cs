using AutoMapper;
using greenharvestbox.Data.Models;
using greenharvestbox.Data.Repositories.LoginRepository;
using greenharvestbox.Data.Repositories.UserRepository;
using greenharvestbox.Data.Resources;
using greenharvestbox.Data.Resources.Messages;
using greenharvestbox.Logic.Models.dto;
using greenharvestbox.Logic.Models.dto.Login.dto;
using greenharvestbox.Logic.Services.Cipher;
using greenharvestbox.Logic.Services.Jwt;
using greenharvestbox.Logic.Services.Mapper;
using greenharvestbox.Logic.Services.UtilityServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Logic.Services.UserServices.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly ILogger<LoginService> _logger;
        private readonly ILoginRepository _loginRepository;
        private readonly IUtilityService _utilityService;
        private readonly ICipherService _cipherService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public LoginService( ICipherService cipherService, IJwtService jwtService,
            ILoginRepository loginRepository, ILogger<LoginService> logger, IUtilityService utilityService)
        {
            _cipherService = cipherService;
            _jwtService = jwtService;

            // Add MapperService profile to define and use Maps
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperService>();
            });
            _mapper = config.CreateMapper();
            _loginRepository = loginRepository;
            _logger = logger;
            _utilityService = utilityService;
        }

        // Map sign up credentials to user entity
        private T Map<T>(object source)
        {
            return _mapper.Map<T>(source);
        }
        // Encrypt user's password to store in database more reliable
        private User HashAndAssignUserPassword(User user, string password)
        {
            _cipherService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            return user;
        }
        // Create a user token depends on which role user had
        private string GenerateTokenBasedOnUserRole(bool isCompanyWorker)
        {
            return isCompanyWorker ?
                _jwtService.CreateToken(Roles.VENDOR) : _jwtService.CreateToken(Roles.CUSTOMER);
        }
    
        // Do user registration to login and use the system. It saves the required user information with additional information to the database.
        public Response<UserBasicDto> Register(UserRegisterDto userRegisterDto)
        {
            _logger.LogInformation("Register function is launched...");
            bool isUserExist = _loginRepository.IsUserExists(userRegisterDto.Email);
            if (isUserExist)
            {
                _logger.LogError(_utilityService.CombineStrings(Error.USER_ALREADY_ADDED, Error.PROCESS_FAILED));
                return _utilityService.CreateResponseMessage(_utilityService.CombineStrings(Error.USER_ALREADY_ADDED, Error.USER_NOT_ADDED), new UserBasicDto(), false);
            }

            // Fill the user entity with necessary information
            User user = Map<User>(userRegisterDto);

            user = HashAndAssignUserPassword(user, userRegisterDto.Password);
            if(user.PasswordHash == null && user.PasswordSalt == null)
            {
                _logger.LogError(_utilityService.CombineStrings(Error.PASSWORD_NOT_ENCRYPTED,Error.PROCESS_FAILED));
                return _utilityService.CreateResponseMessage(_utilityService.CombineStrings(Error.PASSWORD_NOT_ENCRYPTED, Error.USER_NOT_ADDED), new UserBasicDto(), false);
            }
            _logger.LogInformation(Success.PASSWORD_ENCRYPTED);

            user.Token = GenerateTokenBasedOnUserRole(userRegisterDto.IsCompanyWorker);
            if (user.Token == null)
            {
                _logger.LogError(_utilityService.CombineStrings(Error.TOKEN_NOT_CREATED, Error.PROCESS_FAILED));
                return _utilityService.CreateResponseMessage(_utilityService.CombineStrings(Error.TOKEN_NOT_CREATED, Error.USER_NOT_ADDED), new UserBasicDto(), false);
            }
            _logger.LogInformation(Success.TOKEN_CREATED);

            user.RegisteredAt = DateTime.Now;
            // Add user to database
            User? addedUser = _loginRepository.AddUser(user);
            if (addedUser == null)
            {
                _logger.LogInformation(Error.USER_NOT_ADDED);
                return _utilityService.CreateResponseMessage(Error.USER_NOT_ADDED, new UserBasicDto(), false);
            }
            
            // Map user information to UserBasic because we need to simple information on front side to decrease request number
            UserBasicDto userBasicDto = Map<UserBasicDto>(addedUser);

            _logger.LogInformation(Success.USER_ADDED);
            _logger.LogInformation("Register function is finished");
            return _utilityService.CreateResponseMessage(Success.USER_ADDED, userBasicDto, true);
        }
        //To login and use the system.
        public Response<UserBasicDto> Login(UserLoginDto userLoginDto)
        {
            _logger.LogInformation("Login function is launched...");
            User? user = _loginRepository.FindUserByEmail(userLoginDto.Email);    
            if(user == null)
            {
                _logger.LogInformation("Credentials not matched");
                return _utilityService.CreateResponseMessage(Error.USER_NOT_FOUND, new UserBasicDto(), false);
            }
            bool isLogin = _cipherService.VerifyPasswordHash(user.PasswordHash, user.PasswordSalt, userLoginDto.Password);
            if (!isLogin)
            {
                _logger.LogInformation("Credentials not matched");
                return _utilityService.CreateResponseMessage(Error.USER_NOT_FOUND, new UserBasicDto(), false);
            }
            _logger.LogInformation("Credentials are verified");

            UserBasicDto userBasicDto = Map<UserBasicDto>(user);
            _logger.LogInformation("Login function is finished");
            return _utilityService.CreateResponseMessage(Success.USER_LOGIN, userBasicDto, true);

        }
    }
}
