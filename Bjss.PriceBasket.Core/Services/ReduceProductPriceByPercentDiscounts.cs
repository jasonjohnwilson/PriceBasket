using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Services
{
    /// <summary>
    /// Class to represent discounts on individual product items
    /// </summary>
    public class ReduceProductPriceByPercentDiscounts : IBasketDiscounts
    {
        private readonly IGenericRepository<SpecialOffer> _specialOffersRepository;

        public ReduceProductPriceByPercentDiscounts(IGenericRepository<SpecialOffer> specialOffersRepository)
        {
            _specialOffersRepository = specialOffersRepository;
        }

        /// <summary>
        /// Gets all discounts that should be applied to the sale
        /// </summary>
        /// <param name="basket"></param>
        /// <returns>Enumeration of discounts</returns>
        public async Task<IEnumerable<Discount>> GetDiscountsAsync(IBasket basket)
        {
            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            var discounts = new List<Discount>();

            var specialOffers = (await _specialOffersRepository.GetAllAsync(include => include.DiscountedProductsSpecialOffer))
                .Where(o => o.DiscountAssemblyFullName == GetType().AssemblyQualifiedName)
                .Select(s => s.DiscountedProductsSpecialOffer);

            foreach (var offer in specialOffers)
            {
                var discountedProducts = basket.Items.Where(i => i.Id == offer.DiscountedProductId).ToArray();

                if (discountedProducts.Any())
                {
                    var discountsInCurrentOffer = Enumerable.Repeat(new Discount
                    {
                        Name = offer.SpecialOffer.Name,
                        Description = offer.SpecialOffer.Description,
                        DiscountAmount = basket.Items.First(i => i.Id == offer.DiscountedProductId).Price / 100 * offer.DiscountPercent
                    },
                     discountedProducts.Count());

                    discounts.AddRange(discountsInCurrentOffer);
                }
            }

            return discounts;
        }
    }
}
