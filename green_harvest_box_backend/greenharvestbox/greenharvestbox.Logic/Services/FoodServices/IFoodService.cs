﻿using greenharvestbox.Data.Models.dto.Food.dto;
using greenharvestbox.Logic.Models.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Logic.Services.FoodServices
{
    public interface IFoodService
    {
        public Response<List<FoodOverviewDto>> GetRandomFood(int companyId, int amountOfFoodToBrought);
    }
}
