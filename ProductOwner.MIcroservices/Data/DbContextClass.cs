using Microsoft.EntityFrameworkCore;
using ProductOwner.MIcroservices.Model;

namespace ProductOwner.MIcroservices.Data
{
    public class DbContextClass : DbContext
    {

        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options)
        {
        }

        public DbSet<ProductDetails> Products { get; set; }

          public DbSet<Product> Product { get; set; }

    }
}
