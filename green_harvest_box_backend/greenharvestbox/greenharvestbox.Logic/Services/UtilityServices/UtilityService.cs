using greenharvestbox.Data.Resources.Messages;
using greenharvestbox.Logic.Models.dto.Login.dto;
using greenharvestbox.Logic.Models.dto;
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
        public Response<T> CreateResponseMessage<T>(string errorMessage, T data, bool progress)
        {
            return new Response<T>
            {
                Message = errorMessage,
                Data = data,
                Progress = progress
            };
        }
        public string ExceptionInformation(string message, string stacktrace)
        {
            return "Exception message : " + message + "Exception Stack Trace : " + stacktrace;
        }
    }
}
