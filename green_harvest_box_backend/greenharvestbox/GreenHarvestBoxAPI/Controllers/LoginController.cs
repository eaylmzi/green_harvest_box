using greenharvestbox.Data.Models;
using greenharvestbox.Data.Repositories.UserRepository;
using greenharvestbox.Logic.Models.dto;
using greenharvestbox.Logic.Models.dto.Login.dto;
using greenharvestbox.Logic.Services.UserServices.LoginService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GreenHarvestBoxAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        private string ExceptionInformation(string message, string stacktrace)
        {
            return "Exception message : " + message  + "Exception Stack Trace : " + stacktrace;
        }

        [HttpPost]
        public ActionResult<Response<UserBasicDto>> Register([FromBody]UserRegisterDto userRegisterDto)
        {
            try
            {
                return Ok(_loginService.Register(userRegisterDto));
            }
            catch(Exception ex)
            {
                return BadRequest(new Response<UserBasicDto> { Message = ExceptionInformation(ex.Message, ex.StackTrace),
                                                               Data = new UserBasicDto(),
                                                               Progress = false});
            }
            
        }
        [HttpPost]
        public ActionResult<Response<UserBasicDto>> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                return Ok(_loginService.Login(userLoginDto));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<UserBasicDto>
                {
                    Message = ExceptionInformation(ex.Message, ex.StackTrace),
                    Data = new UserBasicDto(),
                    Progress = false
                });
            }

        }
    }
}
