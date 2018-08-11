using Bjss.PriceBasket.Core.Models;
using Bjss.PriceBasket.Infrastructure.DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Bjss.PriceBasket.Infrastructure.DataAccess.Contexts
{
    public class PriceBasketContext : DbContext
    {
        public PriceBasketContext(DbContextOptions<PriceBasketContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<SpecialOffer> SpecialOffers { get; set; }
        public DbSet<BuyTwoGetOneHalfPriceSpecialOffer> BuyTwoGetOneHalfPriceSpecialOffers { get; set; }
        public DbSet<DiscountedProductsSpecialOffer> DiscountedProductsSpecialOffers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<BuyTwoGetOneHalfPriceSpecialOffer>(new BuyTwoGetOneHalfPriceSpecialOfferEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration<DiscountedProductsSpecialOffer>(new DiscountedProductsSpecialOfferEntityTypeConfiguration());
        }
    }
}
