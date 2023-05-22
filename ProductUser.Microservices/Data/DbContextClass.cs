using Microsoft.EntityFrameworkCore;
using ProductUser.Microservices.Model;

namespace ProductUser.Microservices.Data
{
    public class DbContextClass : DbContext
    {

        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options)
        {
        }

        public DbSet<ProductOfferDetail> ProductOffers { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }


    }
}
