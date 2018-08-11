using System;

namespace Bjss.PriceBasket.Core.Models
{
    public class BuyTwoGetOneHalfPriceSpecialOffer : Entity
    {
        public Guid BuyTwoOfProductId { get; set; }
        public Guid DiscountedProductId { get; set; }
        public decimal DiscountPercent { get; set; }
        public Guid SpecialOfferId { get; set; }

        public virtual SpecialOffer SpecialOffer { get; set; }
        public virtual Product BuyTwoOfProduct { get; set; }
        public virtual Product DiscountedProduct { get; set; }
    }
}
