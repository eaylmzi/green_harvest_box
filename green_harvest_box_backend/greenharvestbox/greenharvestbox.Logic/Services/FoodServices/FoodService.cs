using AutoMapper.Internal;
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
        // Summary:
        //     Gets food randomly. If there is no food, it will return empty list.
        public Response<List<FoodOverviewDto>> GetRandomFood(int companyId, int amountOfFoodToBrought)
        {
            List<FoodOverviewDto> foodOverviewList = _foodRepository.GetRandomFood(companyId, amountOfFoodToBrought);
            if (foodOverviewList.IsNullOrEmpty())
            {
                return _utilityService.CreateResponseMessage(Error.FOOD_LIST_EMPTY, new List<FoodOverviewDto>(), false);
            }
            return _utilityService.CreateResponseMessage(Success.FOOD_LIST_BROUGHT, foodOverviewList, true);
        }
        // Summary:
        //     Gets food according to its discount rate. If there is no food, it will return empty list.
        public Response<List<FoodOverviewDto>> GetFoodByDiscountRate(int companyId, int amountOfFoodToBrought)
        {
            List<FoodOverviewDto> foodOverviewList = _foodRepository.GetFoodByDiscountRate(companyId, amountOfFoodToBrought);
            if (foodOverviewList.IsNullOrEmpty())
            {
                return _utilityService.CreateResponseMessage(Error.NO_DISCOUNTED_FOOD, new List<FoodOverviewDto>(), false);
            }
            return _utilityService.CreateResponseMessage(Success.FOOD_LIST_BROUGHT, foodOverviewList, true);
        }
        // Summary:
        //     Gets food according to given name. If given name is not found in list or less than 3 character return empty list.
        public Response<List<FoodOverviewDto>> GetFoodByName(string name)
        {
            List<FoodOverviewDto> foodOverviewList = _foodRepository.GetFoodByName(name);
            if (foodOverviewList.IsNullOrEmpty())
            {
                return _utilityService.CreateResponseMessage(Error.NO_FOOD_FOUND, new List<FoodOverviewDto>(), false);
            }
            return _utilityService.CreateResponseMessage(Success.FOOD_LIST_BROUGHT, foodOverviewList, true);
        }
        // Summary:
        //     Gets food according to given type. If given type is not found in list or less than 3 character return empty list.
        public Response<List<FoodOverviewDto>> GetFoodByType(string name)
        {
            List<FoodOverviewDto> foodOverviewList = _foodRepository.GetFoodByType(name);
            if (foodOverviewList.IsNullOrEmpty())
            {
                return _utilityService.CreateResponseMessage(Error.NO_FOOD_FOUND, new List<FoodOverviewDto>(), false);
            }
            return _utilityService.CreateResponseMessage(Success.FOOD_LIST_BROUGHT, foodOverviewList, true);
        }
        /*
        //Unused function. If the website is avaiable for more than one company, it is logical
        //It returns the features of the food with the most comments.If there aren't enough reviews for the desired amount,
        //random products are added to the list to prevent the screen from remaining empty.
        public Response<List<FoodOverviewDto>> GetFoodByCommentCount(int companyId, int amountOfFoodToBrought)
        {
            //Get the features of food with the most comment
            List<FoodOverviewDto> commentedFoodOverviewList = _foodRepository.GetFoodByCommentCount(companyId, amountOfFoodToBrought);
            if (commentedFoodOverviewList.IsNullOrEmpty())
            {
                Response<List<FoodOverviewDto>> randomFoodOverviewListResponse = GetRandomFood(companyId, amountOfFoodToBrought);
                return randomFoodOverviewListResponse;
            }

            // If the amount of food does not equal to desired amount of food, it will add random food to list
            if(commentedFoodOverviewList.Count != amountOfFoodToBrought)
            {
                List<FoodOverviewDto> foodOverviewList = _foodRepository.GetRandomFood(companyId, amountOfFoodToBrought);
                List<FoodOverviewDto> newMergedFoodOverviewList = commentedFoodOverviewList.Concat(foodOverviewList).Distinct().ToList();
                return _utilityService.CreateResponseMessage(Success.FOOD_LIST_BROUGHT, newMergedFoodOverviewList, true);
            }

            return _utilityService.CreateResponseMessage(Success.FOOD_LIST_BROUGHT, commentedFoodOverviewList, true);
        }
        */

    }
}
