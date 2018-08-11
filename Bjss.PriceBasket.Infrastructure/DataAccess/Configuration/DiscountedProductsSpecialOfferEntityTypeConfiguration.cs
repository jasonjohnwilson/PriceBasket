using Bjss.PriceBasket.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bjss.PriceBasket.Infrastructure.DataAccess.Configuration
{
    public class DiscountedProductsSpecialOfferEntityTypeConfiguration : IEntityTypeConfiguration<DiscountedProductsSpecialOffer>
    {
        public void Configure(EntityTypeBuilder<DiscountedProductsSpecialOffer> builder)
        {
            builder.HasOne(p => p.DiscountedProduct)
                .WithMany()
                .HasForeignKey(p => p.DiscountedProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
