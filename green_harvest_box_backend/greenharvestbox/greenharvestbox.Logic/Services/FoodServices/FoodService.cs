using greenharvestbox.Data.Models.dto.Food.dto;
using greenharvestbox.Data.Repositories.FoodRepository;
using greenharvestbox.Data.Resources.Messages;
using greenharvestbox.Logic.Models.dto;
using greenharvestbox.Logic.Services.UtilityServices;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Logic.Services.FoodServices
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IUtilityService _utilityService;

        public FoodService(IFoodRepository foodRepository, IUtilityService utilityService)
        {
            _foodRepository = foodRepository;
            _utilityService = utilityService;
        }
        public Response<List<FoodOverviewDto>> GetRandomFood(int companyId, int amountOfFoodToBrought)
        {
            List<FoodOverviewDto> foodOverviewList = _foodRepository.GetRandomFood(companyId, amountOfFoodToBrought);
            if (foodOverviewList.IsNullOrEmpty())
            {
                return _utilityService.CreateResponseMessage(Error.FOOD_LIST_EMPTY, new List<FoodOverviewDto>(), false);
            }
            return _utilityService.CreateResponseMessage(Success.FOOD_LIST_BROUGHT, foodOverviewList, true);
        }


    }
}
