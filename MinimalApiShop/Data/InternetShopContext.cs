using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Models.Users;
using MinimalApiShop.Models.Products;

namespace MinimalApiShop.Data
{
    public partial class InternetShopContext : DbContext
    {
        public InternetShopContext()
        {
        }

        public InternetShopContext(DbContextOptions<InternetShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
    }
}
