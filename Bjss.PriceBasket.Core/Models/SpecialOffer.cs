namespace Bjss.PriceBasket.Core.Models
{
    public class SpecialOffer : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DiscountAssemblyFullName { get; set; }

        public virtual BuyTwoGetOneHalfPriceSpecialOffer BuyTwoGetOneHalfPriceSpecialOffer { get; set; }
        public virtual DiscountedProductsSpecialOffer DiscountedProductsSpecialOffer { get; set; }
    }
}
