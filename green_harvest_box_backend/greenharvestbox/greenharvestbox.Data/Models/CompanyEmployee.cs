using System;
using System.Collections.Generic;

namespace greenharvestbox.Data.Models
{
    public partial class CompanyEmployee
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long UserId { get; set; }
    }
}
