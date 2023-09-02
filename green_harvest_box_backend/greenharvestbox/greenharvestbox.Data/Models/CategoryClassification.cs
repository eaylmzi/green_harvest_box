using System;
using System.Collections.Generic;

namespace greenharvestbox.Data.Models
{
    public partial class CategoryClassification
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public string Type { get; set; } = null!;
    }
}
