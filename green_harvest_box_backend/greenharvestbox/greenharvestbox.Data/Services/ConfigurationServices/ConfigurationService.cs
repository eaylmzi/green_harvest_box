using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace greenharvestbox.Data.Services.ConfigurationServices
{


    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath("C:\\Users\\Dell\\Desktop\\GreenHarvestBox\\green_harvest_box_backend\\greenharvestbox\\greenharvestbox.Data")
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        public string? GetMyConnectionString()
        {
            string? connection = _configuration["Connection"];
            if(connection != null)
            {
                return connection;
            }
            return null;
        }
        public string? GetMySecretKey()
        {
            string? connection = _configuration["Token"];
            if (connection != null)
            {
                return connection;
            }
            return null;
        }
    }
}
