using Dapper;
using greenharvestbox.Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Data.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        public User? AddCategory(Category category)
        {
            var procedureName = "Category_AddCategory"; 
            DynamicParameters parameters = new DynamicParameters();

            // Parametreleri DynamicParameters nesnesine ekleyebilirsiniz.
            parameters.Add("@Name", category.Name, DbType.String);


            using (var connection = new SqlConnection(MYSQL))
            {
                User? addedUser = connection.QuerySingleOrDefault<User>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                //
                if (addedUser != null)
                {
                    return addedUser;
                }
                return null;
            }
        }
    }
}
