using System;

namespace Bjss.PriceBasket.Core.Models
{
    public class DiscountedProductsSpecialOffer : Entity
    {
        public Guid DiscountedProductId { get; set; }
        public decimal DiscountPercent { get; set; }
        public Guid SpecialOfferId { get; set; }

        public virtual SpecialOffer SpecialOffer { get; set; }
        public virtual Product DiscountedProduct { get; set; }
    }
}
