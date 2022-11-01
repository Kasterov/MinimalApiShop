using Microsoft.EntityFrameworkCore;
using MinimalApiShop.Models;

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
    }
}
