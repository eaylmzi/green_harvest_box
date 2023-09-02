using greenharvestbox.Data.Models;
using greenharvestbox.Data.Repositories.UserRepository;
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

        [HttpPost]
        public ActionResult<User> Register(string name)
        {
            return Ok();
        }
    }
}
