using greenharvestbox.Data.Models;
using greenharvestbox.Data.Models.dto.Food.dto;
using greenharvestbox.Data.Repositories.FoodRepository;
using greenharvestbox.Data.Resources.Messages;
using greenharvestbox.Logic.Models.dto;
using greenharvestbox.Logic.Services.FoodServices;
using greenharvestbox.Logic.Services.UtilityServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Test
{
    public class FoodTest
    {
        private readonly FoodService _foodService;
        private readonly UtilityService _utilityService;
        private readonly Mock<IFoodRepository> _foodRepositoryMock = new Mock<IFoodRepository>();
        private readonly Mock<IUtilityService> _utilityServiceMock = new Mock<IUtilityService>();
        

        public FoodTest() {
            _utilityService = new UtilityService();
            _foodService = new FoodService(_foodRepositoryMock.Object, _utilityService);
            
        }
        [Fact]
        public void GetRandomFood_ShouldReturnSuccessResponse_WhenParametersAreValid()
        {
            //ARRANGE
            Logic.Models.dto.Food.dto.FoodRequestDto foodRequestDto = new Logic.Models.dto.Food.dto.FoodRequestDto()
            {
                CompanyId = 1,
                AmountOfFoodToBrought = 1
            };
            List<FoodOverviewDto> foodOverviewDtoList = new List<FoodOverviewDto>();
            FoodOverviewDto foodOverviewDto = new FoodOverviewDto()
            {
                Id = 1,
                FoodName = "çilek",
                CategoryName = "meyve",
                Type = "yaz meyvesi",
                Price = 10,
                Quantity = 20,
                Discount = 0,
                Content = null
            };
            foodOverviewDtoList.Add(foodOverviewDto);
            _foodRepositoryMock.Setup(x => x.GetRandomFood(foodRequestDto.CompanyId, foodRequestDto.AmountOfFoodToBrought)).Returns(foodOverviewDtoList);
            //ACT

            Response<List<FoodOverviewDto>> response = _foodService.GetRandomFood(foodRequestDto.CompanyId, foodRequestDto.AmountOfFoodToBrought);
            
            Assert.Equal(Success.FOOD_LIST_BROUGHT, response.Message);
            Assert.NotNull(response.Data);
            Assert.True(response.Progress);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        public void GetRandomFood_ShouldReturnErrorResponse_WhenFoodListEmptyOrFoodAmountZero(int foodAmount)
        {
            //ARRANGE
            Logic.Models.dto.Food.dto.FoodRequestDto foodRequestDto = new Logic.Models.dto.Food.dto.FoodRequestDto()
            {
                CompanyId = 1,
                AmountOfFoodToBrought = foodAmount
            };
            List<FoodOverviewDto> foodOverviewDtoList = new List<FoodOverviewDto>();

            _foodRepositoryMock.Setup(x => x.GetRandomFood(foodRequestDto.CompanyId, foodRequestDto.AmountOfFoodToBrought)).Returns(foodOverviewDtoList);
            //ACT

            Response<List<FoodOverviewDto>> response = _foodService.GetRandomFood(foodRequestDto.CompanyId, foodRequestDto.AmountOfFoodToBrought);

            Assert.Equal(Error.FOOD_LIST_EMPTY, response.Message);
            Assert.Empty(response.Data);
            Assert.False(response.Progress);
        }

        [Fact]
        public void GetFoodByDiscountRate_ShouldReturnSuccessResponse_WhenParametersAreValid()
        {
            //ARRANGE
            Logic.Models.dto.Food.dto.FoodRequestDto foodRequestDto = new Logic.Models.dto.Food.dto.FoodRequestDto()
            {
                CompanyId = 1,
                AmountOfFoodToBrought = 1
            };
            List<FoodOverviewDto> foodOverviewDtoList = new List<FoodOverviewDto>();
            FoodOverviewDto foodOverviewDto = new FoodOverviewDto()
            {
                Id = 1,
                FoodName = "çilek",
                CategoryName = "meyve",
                Type = "yaz meyvesi",
                Price = 10,
                Quantity = 20,
                Discount = 10,
                Content = null
            };
            foodOverviewDtoList.Add(foodOverviewDto);
            _foodRepositoryMock.Setup(x => x.GetFoodByDiscountRate(foodRequestDto.CompanyId, foodRequestDto.AmountOfFoodToBrought)).Returns(foodOverviewDtoList);
            //ACT

            Response<List<FoodOverviewDto>> response = _foodService.GetFoodByDiscountRate(foodRequestDto.CompanyId, foodRequestDto.AmountOfFoodToBrought);

            Assert.Equal(Success.FOOD_LIST_BROUGHT, response.Message);
            Assert.NotNull(response.Data);
            Assert.True(response.Progress);
            Assert.True(foodOverviewDto.Discount > 0 && foodOverviewDto.Discount < 100);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        public void GetFoodByDiscountRate_ShouldReturnErrorResponse_WhenFoodListEmptyOrFoodAmountZero(int foodAmount)
        {
            //ARRANGE
            Logic.Models.dto.Food.dto.FoodRequestDto foodRequestDto = new Logic.Models.dto.Food.dto.FoodRequestDto()
            {
                CompanyId = 1,
                AmountOfFoodToBrought = foodAmount
            };
            List<FoodOverviewDto> foodOverviewDtoList = new List<FoodOverviewDto>();

            _foodRepositoryMock.Setup(x => x.GetFoodByDiscountRate(foodRequestDto.CompanyId, foodRequestDto.AmountOfFoodToBrought)).Returns(foodOverviewDtoList);
            //ACT

            Response<List<FoodOverviewDto>> response = _foodService.GetFoodByDiscountRate(foodRequestDto.CompanyId, foodRequestDto.AmountOfFoodToBrought);

            Assert.Equal(Error.NO_DISCOUNTED_FOOD, response.Message);
            Assert.Empty(response.Data);
            Assert.False(response.Progress);
        }
    }
}
