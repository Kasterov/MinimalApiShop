using System;
using System.Collections.Generic;

namespace MinimalApiShop.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Category Category { get; set; }
        public string? Atribute { get; set; }
        public int Quantity { get; set; }
    }
}
