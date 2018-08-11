using Bjss.PriceBasket.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bjss.PriceBasket.Infrastructure.DataAccess.Configuration
{
    class BuyTwoGetOneHalfPriceSpecialOfferEntityTypeConfiguration : IEntityTypeConfiguration<BuyTwoGetOneHalfPriceSpecialOffer>
    {
        public void Configure(EntityTypeBuilder<BuyTwoGetOneHalfPriceSpecialOffer> builder)
        {
            builder.HasOne(p => p.BuyTwoOfProduct)
                .WithMany()
                .HasForeignKey(p => p.BuyTwoOfProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.DiscountedProduct)
                .WithMany()
                .HasForeignKey(p => p.DiscountedProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
