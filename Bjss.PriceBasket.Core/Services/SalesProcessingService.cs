using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Services
{
    /// <summary>
    /// Service that drives the process of validating, calculating discounts and determine sales details
    /// </summary>
    public class SalesProcessingService : ISalesProcessingService
    {
        private readonly IBasketValidator _basketValidator;
        private readonly IBasketFactory _basketFactory;
        private readonly ISaleCalculator _saleCalculator;

        public SalesProcessingService(
            IBasketValidator basketValidator,
            IBasketFactory basketFactor,
            ISaleCalculator saleCalculator)
        {
            _basketValidator = basketValidator;
            _basketFactory = basketFactor;
            _saleCalculator = saleCalculator;
        }
        
        public async Task<SalesProcessingResult> TryCreateSaleAsync(string [] selectedProducts)
        {   
            var salesProcessingResult = new SalesProcessingResult { Success = false };
            var validationErrors = await _basketValidator.ValidateAsync(selectedProducts);

            if (validationErrors.Any())
            {
                salesProcessingResult.ValidationErrors = validationErrors;
            }
            else
            {
                var basket = _basketFactory.Get(selectedProducts);
                salesProcessingResult.SaleDetails = await _saleCalculator.CalculateAsync(basket);
                salesProcessingResult.Success = true;
            }

            return salesProcessingResult;
        }
    }
}
