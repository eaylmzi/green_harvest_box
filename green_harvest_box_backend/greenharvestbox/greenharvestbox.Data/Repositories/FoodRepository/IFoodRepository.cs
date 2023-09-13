using greenharvestbox.Data.Models.dto.Food.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Data.Repositories.FoodRepository
{
    public interface IFoodRepository
    {
        public List<FoodOverviewDto> GetRandomFood(int companyId, int amountOfFoodToBrought);
        public List<FoodOverviewDto> GetFoodByDiscountRate(int companyId, int amountOfFoodToBrought);
        // public List<FoodOverviewDto> GetFoodByCommentCount(int companyId, int amountOfFoodToBrought);
        public List<FoodOverviewDto> GetFoodByName(string name);
    }
}
