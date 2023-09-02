using System;
using System.Collections.Generic;

namespace greenharvestbox.Data.Models
{
    public partial class Order
    {
        public long Id { get; set; }
        public long FoodId { get; set; }
        public string Status { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
