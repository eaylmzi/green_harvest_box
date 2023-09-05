using AutoMapper;
using Dapper;
using greenharvestbox.Data.Models;
using greenharvestbox.Data.Services.ConfigurationServices;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Data.Repositories.LoginRepository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfigurationService _configurationService;
        private readonly string ConnectionString;
        public LoginRepository(IConfigurationService configurationService)
        {

            _configurationService = configurationService;
            ConnectionString = _configurationService.GetMyConnectionString();
        }
        public User? AddUser(User user)
        {
            var procedureName = "User_AddUser";
            DynamicParameters parameters = new DynamicParameters();

            // Parametreleri DynamicParameters nesnesine ekleyebilirsiniz.
            parameters.Add("@Name", user.Name, DbType.String);
            parameters.Add("@Surname", user.Surname, DbType.String);
            parameters.Add("@Email", user.Email, DbType.String);
            parameters.Add("@PhoneNumber", user.PhoneNumber, DbType.String);
            parameters.Add("@PasswordHash", user.PasswordHash, DbType.Binary);
            parameters.Add("@PasswordSalt", user.PasswordSalt, DbType.Binary);
            parameters.Add("@City", user.City, DbType.String);
            parameters.Add("@Province", user.Province, DbType.String);
            parameters.Add("@Address", user.Address, DbType.String);
            parameters.Add("@RegisteredAt", user.RegisteredAt, DbType.DateTime);
            parameters.Add("@Token", user.Token, DbType.String);

            using (var connection = new SqlConnection(ConnectionString))
            {
                User addedUser = connection.QuerySingleOrDefault<User>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                if (addedUser != null)
                {
                    return addedUser;
                }
                return null;
            }
        }
        public User? GetUser(int id)
        {
            var procedureName = "User_GetUser";
            DynamicParameters parameters = new DynamicParameters();

            // Parametreleri DynamicParameters nesnesine ekleyebilirsiniz.
            parameters.Add("@Id", id, DbType.Int64);

            using (var connection = new SqlConnection(ConnectionString))
            {
                User user = connection.QuerySingleOrDefault<User>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                if (user != null)
                {
                    return user;
                }
                return null;
            }
        }
        public User? FindUserByEmailAndPassword(string email, byte[] passwordHash, byte[] passwordSalt)
        {
            var procedureName = "User_FindUserByEmailAndPassword";
            DynamicParameters parameters = new DynamicParameters();

            // Parametreleri DynamicParameters nesnesine ekleyebilirsiniz.
            parameters.Add("@Email", email, DbType.String);
            parameters.Add("@PasswordHash", passwordHash, DbType.Binary);
            parameters.Add("@PasswordSalt", passwordSalt, DbType.Binary);

            using (var connection = new SqlConnection(ConnectionString))
            {
                User user = connection.QuerySingleOrDefault<User>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                if (user != null)
                {
                    return user;
                }
                return null;
            }
        }
        public User? FindUserByEmail(string email)
        {
            var procedureName = "User_FindUserByEmail";
            DynamicParameters parameters = new DynamicParameters();

            // Parametreleri DynamicParameters nesnesine ekleyebilirsiniz.
            parameters.Add("@Email", email, DbType.String);

            using (var connection = new SqlConnection(ConnectionString))
            {
                User user = connection.QuerySingleOrDefault<User>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                if (user != null)
                {
                    return user;
                }
                return null;
            }
        }
        public bool IsUserExists(string email)
        {
            var procedureName = "User_IsUserExists";
            DynamicParameters parameters = new DynamicParameters();

            // Parametreleri DynamicParameters nesnesine ekleyebilirsiniz.
            parameters.Add("@Email", email, DbType.String);

            using (var connection = new SqlConnection(ConnectionString))
            {
                string userEmail = connection.QuerySingleOrDefault<string>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                if (userEmail != null)
                {
                    return true;
                }
                return false;
            }
        }

    }
}
