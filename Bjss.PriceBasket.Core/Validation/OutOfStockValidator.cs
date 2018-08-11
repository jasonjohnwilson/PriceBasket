using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;

namespace Bjss.PriceBasket.Core.Validation
{
    /// <summary>
    /// Validates to ensure that the product is not out of stock
    /// </summary>
    public class OutOfStockValidator : IBasketValidator
    {
        private readonly IGenericRepository<Product> _productRepository;

        public OutOfStockValidator(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ValidationError>> ValidateAsync(IEnumerable<string> products)
        {
            var validationErrors = new List<ValidationError>();

            foreach (var product in products)
            {
                var productModel = (await _productRepository
                                        .GetAllAsync(p => p.Name.Equals(product, StringComparison.InvariantCultureIgnoreCase)))
                                        .FirstOrDefault();

                if (productModel != null && productModel.QuantityAvailable == 0)
                {
                    validationErrors.Add(ValidationError.OutOfStock(product));
                }
            }

            return validationErrors;
        }
    }
}
