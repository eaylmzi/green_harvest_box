using System;
using System.Collections.Generic;

namespace greenharvestbox.Data.Models
{
    public partial class User
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string? Address { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string Token { get; set; } = null!;
    }
}
