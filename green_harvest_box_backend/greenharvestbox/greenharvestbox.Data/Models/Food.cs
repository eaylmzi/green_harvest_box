using System;
using System.Collections.Generic;

namespace greenharvestbox.Data.Models
{
    public partial class Food
    {
        public long Id { get; set; }
        public long CategoryClassificationId { get; set; }
        public string Name { get; set; } = null!;
        public long FoodDetailId { get; set; }
    }
}
