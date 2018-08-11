using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Services
{
    /// <summary>
    /// Processes all the available discounts against the items in the basket
    /// </summary>
    public class BasketDiscountProcessor : IBasketDiscountProcessor
    {
        private readonly IBasketDiscountProviderFactory _basketDiscountProviderFactory;

        public BasketDiscountProcessor(IBasketDiscountProviderFactory basketDiscountProviderFactory)
        {
            _basketDiscountProviderFactory = basketDiscountProviderFactory;
        }

        /// <summary>
        /// Get all the discounts that can apply to the current basket
        /// </summary>
        /// <param name="basket">The basket containing all the products the user wants to purchase</param>
        /// <returns>Aggregation of all the discounts that can application to the basket</returns>
        public async Task<IEnumerable<Discount>> GetAllDiscountsAsync(IBasket basket)
        {
            if(basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            var discounts = new List<Discount>();

            foreach(var discountProvider in await _basketDiscountProviderFactory.GetAsync())
            {
                discounts.AddRange(await discountProvider.GetDiscountsAsync(basket));
            }

            return discounts;
        }
    }
}
