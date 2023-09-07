using greenharvestbox.Logic.Models.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Logic.Services.UtilityServices
{
    public interface IUtilityService
    {
        public string CombineStrings(string firstString, string secondString);
        public Response<T> CreateResponseMessage<T>(string errorMessage, T data, bool progress);
        public string ExceptionInformation(string message, string stacktrace);

    }
}
