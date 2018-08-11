using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Services
{
    /// <summary>
    /// Calculates the overall sale details including total, sub total and any discounts
    /// </summary>
    public class SaleCalculator : ISaleCalculator
    {
        private readonly IBasketDiscountProcessor _basketDiscountProcessor;

        public SaleCalculator(IBasketDiscountProcessor basketDiscountProcessor)
        {
            _basketDiscountProcessor = basketDiscountProcessor;
        }

        /// <summary>
        /// Calculates the sales details for the current basket
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        public async Task<SaleDetails> CalculateAsync(IBasket basket)
        {
            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            var discounts = await _basketDiscountProcessor.GetAllDiscountsAsync(basket);
            var discountToDeduct = discounts.Sum(d => d.DiscountAmount);
            var subTotal = basket.Items.Sum(i => i.Price);
            var total = subTotal - discountToDeduct;

            return new SaleDetails {
                SubTotal = subTotal,
                Total = total, 
                Discounts = discounts
            };
        }
    }
}
