using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApiShop.Models.Products
{
    public partial class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Category Category { get; set; }
        public string? Atribute { get; set; }
        public int Quantity { get; set; }
    }
}
