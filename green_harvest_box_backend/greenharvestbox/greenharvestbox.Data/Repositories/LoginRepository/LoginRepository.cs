using Dapper;
using greenharvestbox.Data.Models;
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
        private readonly string MYSQL = "Server=.\\SQLSERVER;Database=GreenHarvestBoxDB;Trusted_Connection=True;";
        public User? AddUser(User user)
        {
            var sql = "INSERT INTO user (name, surname, city_id, email, phone_number, password_hash, password_salt, city, province, address, registered_at, token)" +
                " VALUES(@Name, @Surname, @Email, @PhoneNumber, @PasswordHash, @PasswordSalt, @City, @Province, @Address, @RegisteredAt, @Token )";
            DynamicParameters parameters = new DynamicParameters();

            // Parametreleri DynamicParameters nesnesine ekleyebilirsiniz.
            parameters.Add("@Name", user.Name, DbType.String);
            parameters.Add("@Surname", user.Surname, DbType.String);
            parameters.Add("@Email", user.Email, DbType.String);
            parameters.Add("@PhoneNumber", user.PhoneNumber, DbType.String);
            parameters.Add("@PasswordHash", user.PasswordHash, DbType.Byte);
            parameters.Add("@PasswordSalt", user.PasswordSalt, DbType.Byte);
            parameters.Add("@City", user.City, DbType.String);
            parameters.Add("@Province", user.Province, DbType.String);
            parameters.Add("@Address", user.Address, DbType.String);
            parameters.Add("@RegisteredAt", user.RegisteredAt, DbType.DateTime);
            parameters.Add("@Token", user.Token, DbType.String);

            using (var connection = new SqlConnection(MYSQL))
            {
                User? addedUser = connection.QuerySingleOrDefault<User>(sql, parameters);
                //, commandType: CommandType.StoredProcedure
                if (addedUser != null)
                {
                    return addedUser;
                }
                return null;
            }
        }
    }
}
