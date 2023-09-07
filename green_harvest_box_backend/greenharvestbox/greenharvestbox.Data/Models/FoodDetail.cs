using System;
using System.Collections.Generic;

namespace greenharvestbox.Data.Models
{
    public partial class FoodDetail
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
        public bool IsInStock { get; set; }
        public string Slug { get; set; } = null!;
        public string? Content { get; set; } 
        public bool IsDeleted { get; set; }
        public double QuantitySold { get; set; }

    }
}
