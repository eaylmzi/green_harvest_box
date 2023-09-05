using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Logic.Services.UtilityServices
{
    public class UtilityService : IUtilityService
    {
        public string CombineStrings(string firstString, string secondString)
        {
            return firstString + ". " + secondString;
        }
    }
}
