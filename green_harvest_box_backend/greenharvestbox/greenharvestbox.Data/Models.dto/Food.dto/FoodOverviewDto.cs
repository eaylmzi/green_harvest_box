using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Data.Models.dto.Food.dto
{
    public class FoodOverviewDto
    {
        public long Id { get; set; }
        public string FoodName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string? Type { get; set; } 
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
        public string? Content { get; set; }
    }
}
