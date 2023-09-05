using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Data.Services.ConfigurationServices
{
    public interface IConfigurationService
    {
        public string? GetMyConnectionString();
        public string? GetMySecretKey();
    }
}
