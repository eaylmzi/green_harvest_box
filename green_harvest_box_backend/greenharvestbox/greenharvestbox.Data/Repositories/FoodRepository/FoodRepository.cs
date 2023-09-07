using Dapper;
using greenharvestbox.Data.Models.dto.Food.dto;
using greenharvestbox.Data.Services.ConfigurationServices;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Data.Repositories.FoodRepository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly IConfigurationService _configurationService;
        private readonly string ConnectionString;
        public FoodRepository(IConfigurationService configurationService)
        {

            _configurationService = configurationService;
            ConnectionString = _configurationService.GetMyConnectionString();
        }

        public List<FoodOverviewDto> GetRandomFood(int companyId, int amountOfFoodToBrought)
        {
            var procedureName = "Food_GetRandomFood";

            // Dapper sorgusu ile verileri çekiyoruz

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId, DbType.Int64);
            parameters.Add("@AmountOfFoodToBrought", amountOfFoodToBrought, DbType.Int32);
            using (var connection = new SqlConnection(ConnectionString))
            {
                List<FoodOverviewDto> foodOverviews = connection.Query<FoodOverviewDto>(procedureName, parameters, commandType: CommandType.StoredProcedure).ToList();              
                return foodOverviews;
            }
        }
    }
}
