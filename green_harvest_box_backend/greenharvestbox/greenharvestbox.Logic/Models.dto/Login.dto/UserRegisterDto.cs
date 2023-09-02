using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Logic.Models.dto.Login.dto
{
    public class UserRegisterDto
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Province { get; set; } = null!;
    }
}
