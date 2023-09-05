using greenharvestbox.Data.Models;
using greenharvestbox.Logic.Models.dto;
using greenharvestbox.Logic.Models.dto.Login.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Logic.Services.UserServices.LoginService
{
    public interface ILoginService
    {
        public Response<UserBasicDto> Register(UserRegisterDto userRegisterDto);
        public Response<UserBasicDto> Login(UserLoginDto userLoginDto);
    }
}
