using System;
using System.Collections.Generic;

namespace greenharvestbox.Data.Models
{
    public partial class Cart
    {
        public long Id { get; set; }
        public long FoodId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime OrderedAt { get; set; }

    }
}
