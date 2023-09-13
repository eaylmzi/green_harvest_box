using greenharvestbox.Data.Models.dto.Food.dto;
using greenharvestbox.Logic.Models.dto.Login.dto;
using greenharvestbox.Logic.Models.dto;
using greenharvestbox.Logic.Services.FoodServices;
using Microsoft.AspNetCore.Mvc;
using greenharvestbox.Logic.Services.UtilityServices;
using greenharvestbox.Logic.Models.dto.Food.dto;
using Microsoft.AspNetCore.Authorization;
using greenharvestbox.Data.Resources;
using FoodOverviewDto = greenharvestbox.Logic.Models.dto.Food.dto.FoodOverviewDto;

namespace GreenHarvestBoxAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FoodController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly IUtilityService _utilityService;
        //[HttpPost, Authorize(Roles = $"{Roles.CUSTOMER}")]
        public FoodController(IFoodService foodService, IUtilityService utilityService)
        {
            _foodService = foodService;
            _utilityService = utilityService;
        }
        [HttpPost]
      
        public ActionResult<List<FoodOverviewDto>> GetRandomFood([FromBody]FoodRequestDto foodRequestDto)
        {
            try
            {
                return Ok(_foodService.GetRandomFood(foodRequestDto.CompanyId,foodRequestDto.AmountOfFoodToBrought));
            }
            catch(Exception ex)
            {
                return BadRequest(_utilityService.CreateResponseMessage(_utilityService.ExceptionInformation(ex.Message, ex.StackTrace), new List<FoodOverviewDto>(), false));
            }
        }
        [HttpPost]

        public ActionResult<List<FoodOverviewDto>> GetFoodByDiscountRate([FromBody] FoodRequestDto foodRequestDto)
        {
            try
            {
                return Ok(_foodService.GetFoodByDiscountRate(foodRequestDto.CompanyId, foodRequestDto.AmountOfFoodToBrought));
            }
            catch (Exception ex)
            {
                return BadRequest(_utilityService.CreateResponseMessage(_utilityService.ExceptionInformation(ex.Message, ex.StackTrace), new List<FoodOverviewDto>(), false));
            }
        }
        [HttpPost]
        public ActionResult<List<FoodOverviewDto>> GetFoodByName([FromBody] FoodNameDto foodNameDto)
        {
            try
            {
                return Ok(_foodService.GetFoodByName(foodNameDto.Name));
            }
            catch (Exception ex)
            {
                return BadRequest(_utilityService.CreateResponseMessage(_utilityService.ExceptionInformation(ex.Message, ex.StackTrace), new List<FoodOverviewDto>(), false));
            }
        }

        /*
        [HttpPost, Authorize(Roles = $"{Roles.CUSTOMER}")]

        public ActionResult<List<FoodOverviewDto>> GetFoodByCommentCount([FromBody] FoodRequestDto foodRequestDto)
        {
            try
            {
                return Ok(_foodService.GetFoodByCommentCount(foodRequestDto.CompanyId, foodRequestDto.AmountOfFoodToBrought));
            }
            catch (Exception ex)
            {
                return BadRequest(_utilityService.CreateResponseMessage(_utilityService.ExceptionInformation(ex.Message, ex.StackTrace), new List<FoodOverviewDto>(), false));
            }
        }
        */
    }
}
