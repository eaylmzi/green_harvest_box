using greenharvestbox.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Data.Repositories.LoginRepository
{
    public interface ILoginRepository
    {
        public User? AddUser(User user);
        public User? FindUserByEmailAndPassword(string email, byte[] passwordHash, byte[] passwordSalt);
        public User? FindUserByEmail(string email);
        public bool IsUserExists(string email);
    }
}
