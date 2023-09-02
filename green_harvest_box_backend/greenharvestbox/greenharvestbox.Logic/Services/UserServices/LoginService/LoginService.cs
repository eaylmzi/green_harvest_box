using greenharvestbox.Data.Models;
using greenharvestbox.Data.Repositories.UserRepository;
using greenharvestbox.Logic.Models.dto.L.dto;
using greenharvestbox.Logic.Models.dto.Login.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Logic.Services.UserServices.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Register(UserRegisterDto userRegisterDto)
        {
            return new User();
        }
    }
}
