using System;
using System.Collections.Generic;

namespace greenharvestbox.Data.Models
{
    public partial class Company
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string Intro { get; set; } = null!;
    }
}
