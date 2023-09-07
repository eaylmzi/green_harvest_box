using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Data.Resources.Messages
{
    public static class Error
    {
        //For login service
        public const string USER_NOT_ADDED = "User not added";
        public const string USER_ALREADY_ADDED = "There is a user with this email";
        public const string USER_NOT_FOUND = "User credentials is not matched";
        public const string PASSWORD_NOT_ENCRYPTED = "Password not encrypted";
        public const string PROCESS_FAILED = "Process is failed...";
        public const string TOKEN_NOT_CREATED = "User token not created";

        //Food
        public const string FOOD_LIST_EMPTY = "Food list is empty";
    }
}
