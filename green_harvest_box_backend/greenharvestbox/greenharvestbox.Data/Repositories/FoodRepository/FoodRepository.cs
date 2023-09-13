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


            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId, DbType.Int64);
            parameters.Add("@AmountOfFoodToBrought", amountOfFoodToBrought, DbType.Int32);
            using (var connection = new SqlConnection(ConnectionString))
            {
                List<FoodOverviewDto> foodOverviews = connection.Query<FoodOverviewDto>(procedureName, parameters, commandType: CommandType.StoredProcedure).ToList();              
                return foodOverviews;
            }
        }

        public List<FoodOverviewDto> GetFoodByDiscountRate(int companyId, int amountOfFoodToBrought)
        {
            var procedureName = "Food_GetFoodByDiscountRate";


            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId, DbType.Int64);
            parameters.Add("@AmountOfFoodToBrought", amountOfFoodToBrought, DbType.Int32);
            using (var connection = new SqlConnection(ConnectionString))
            {
                List<FoodOverviewDto> foodOverviews = connection.Query<FoodOverviewDto>(procedureName, parameters, commandType: CommandType.StoredProcedure).ToList();
                return foodOverviews;
            }
        }


        /*
        //Unused function. If the website is avaiable for more than one company, it is logical
        //It returns the features of the product with the most comments.
        public List<FoodOverviewDto> GetFoodByCommentCount(int companyId, int amountOfFoodToBrought)
        {
            var procedureName = "Food_GetFoodByCommentCount";

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
        */


        public List<FoodOverviewDto> GetFoodByName(string name)
        {
            var procedureName = "Food_GetFoodByName";


            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", name, DbType.String);
            using (var connection = new SqlConnection(ConnectionString))
            {
                List<FoodOverviewDto> foodOverviews = connection.Query<FoodOverviewDto>(procedureName, parameters, commandType: CommandType.StoredProcedure).ToList();
                return foodOverviews;
            }
        }
        public List<FoodOverviewDto> GetFoodByType(string name)
        {
            var procedureName = "Food_GetFoodByType";


            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", name, DbType.String);
            using (var connection = new SqlConnection(ConnectionString))
            {
                List<FoodOverviewDto> foodOverviews = connection.Query<FoodOverviewDto>(procedureName, parameters, commandType: CommandType.StoredProcedure).ToList();
                return foodOverviews;
            }
        }
    }
} 
