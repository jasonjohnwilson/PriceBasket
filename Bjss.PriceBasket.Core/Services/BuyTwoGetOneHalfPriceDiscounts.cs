using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Services
{
    /// <summary>
    /// Discount processor class for Buy Two Get One Discounted
    /// Currently second product is 50% but it can handle any discount percentage
    /// </summary>
    public class BuyTwoGetOneHalfPriceDiscounts : IBasketDiscounts
    {
        private readonly IGenericRepository<SpecialOffer> _specialOfferRepository;

        public BuyTwoGetOneHalfPriceDiscounts(IGenericRepository<SpecialOffer> specialOfferRepository)
        {
            _specialOfferRepository = specialOfferRepository;
        }

        /// <summary>
        /// Gets all the discounts of this type for the current basket
        /// </summary>
        /// <param name="basket">The basket of products</param>
        /// <returns>The discounts to apply to the sale</returns>
        public async Task<IEnumerable<Discount>> GetDiscountsAsync(IBasket basket)
        {
            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            var discounts = new List<Discount>();

            var specialOffers = (await _specialOfferRepository.GetAllAsync(include => include.BuyTwoGetOneHalfPriceSpecialOffer))
               .Where(o => o.DiscountAssemblyFullName == GetType().AssemblyQualifiedName)
               .Select(s => s.BuyTwoGetOneHalfPriceSpecialOffer);

            foreach (var offer in specialOffers)

            {
                var buyTwoItemCount = basket.Items.Count(i => i.Id == offer.BuyTwoOfProductId);
                var halfPriceItems = basket.Items.Count(i => i.Id == offer.DiscountedProductId);
                var numberOfItemsNotInDiscount = halfPriceItems - (buyTwoItemCount / 2);
                var numberOfItemsToDiscount = halfPriceItems - (numberOfItemsNotInDiscount > 0 ? numberOfItemsNotInDiscount : 0);

                if (numberOfItemsToDiscount > 0)
                {
                    var discountsInCurrentOffer = Enumerable.Repeat(new Discount
                    {
                        Name = offer.SpecialOffer.Name,
                        Description = offer.SpecialOffer.Description,
                        DiscountAmount = basket.Items.First(i => i.Id == offer.DiscountedProductId).Price / 100 * offer.DiscountPercent
                    },
                     numberOfItemsToDiscount);

                    discounts.AddRange(discountsInCurrentOffer);
                }
            }

            return discounts;
        }
    }
}
