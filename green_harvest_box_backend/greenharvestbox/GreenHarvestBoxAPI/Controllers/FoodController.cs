using greenharvestbox.Data.Models.dto.Food.dto;
using greenharvestbox.Logic.Models.dto.Login.dto;
using greenharvestbox.Logic.Models.dto;
using greenharvestbox.Logic.Services.FoodServices;
using Microsoft.AspNetCore.Mvc;
using greenharvestbox.Logic.Services.UtilityServices;
using greenharvestbox.Logic.Models.dto.Food.dto;
using Microsoft.AspNetCore.Authorization;
using greenharvestbox.Data.Resources;

namespace GreenHarvestBoxAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FoodController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly IUtilityService _utilityService;

        public FoodController(IFoodService foodService, IUtilityService utilityService)
        {
            _foodService = foodService;
            _utilityService = utilityService;
        }
        [HttpPost, Authorize(Roles = $"{Roles.CUSTOMER}")]
      
        public ActionResult<List<greenharvestbox.Data.Models.dto.Food.dto.FoodOverviewDto>> GetRandomFood([FromBody]FoodRequestDto foodRequestDto)
        {
            try
            {
                return Ok(_foodService.GetRandomFood(foodRequestDto.CompanyId,foodRequestDto.AmountOfFoodToBrought));
            }
            catch(Exception ex)
            {
                return base.BadRequest(new Response<List<greenharvestbox.Data.Models.dto.Food.dto.FoodOverviewDto>>
                {
                    Message = _utilityService.ExceptionInformation(ex.Message, ex.StackTrace),
                    Data = new List<greenharvestbox.Data.Models.dto.Food.dto.FoodOverviewDto>(),
                    Progress = false
                });
            }
        }
    }
}
