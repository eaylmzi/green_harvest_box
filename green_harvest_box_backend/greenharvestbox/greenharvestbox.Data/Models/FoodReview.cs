using System;
using System.Collections.Generic;

namespace greenharvestbox.Data.Models
{
    public partial class FoodReview
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long FoodId { get; set; }
        public string Title { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public short Rating { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
