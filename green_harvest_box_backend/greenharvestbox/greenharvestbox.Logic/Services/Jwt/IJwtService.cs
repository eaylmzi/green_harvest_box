
namespace greenharvestbox.Logic.Services.Jwt
{
    public interface IJwtService
    {
        public string CreateToken(string role);
        //public bool validateToken(string token, IConfiguration _configuration);
        //public int GetUserIdFromToken(IHeaderDictionary headers);
        //public string GetUserTokenFromToken(IHeaderDictionary headers);
        //public string GetUserNameFromToken(IHeaderDictionary headers);
        //public string GetUserRoleFromToken(IHeaderDictionary headers);
        //public PassengerVerifying GetUserInformation(IHeaderDictionary headers);


    }
}
