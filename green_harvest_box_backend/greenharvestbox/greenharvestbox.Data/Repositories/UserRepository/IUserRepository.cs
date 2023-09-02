using greenharvestbox.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Data.Repositories.UserRepository
{
    public interface IUserRepository
    {
        public User? AddCategory(Category category);     
    }
}
